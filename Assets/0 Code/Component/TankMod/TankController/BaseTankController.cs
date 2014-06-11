using UnityEngine;
using System.Collections;

public abstract class BaseTankController : BaseTankMod {
	float steerTimer;

	Transform myTransform;
	
	//current input state
	public float brake;
	public float throttle;
	public float steering;
	float oldSteering;
	float deltaSteering;
	
	float steeringVelo;
	float maxSteer=1;
	float maxThrottle=1;
	
	[HideInInspector]
	public bool brakeKey;
	[HideInInspector]
	public bool accelKey;
	
	[HideInInspector]
	public float steerInput;
	[HideInInspector]
	public float brakeInput;
	[HideInInspector]
	public float throttleInput;
	[HideInInspector]
	public float handbrakeInput;
	[HideInInspector]
	public float clutchInput;
	[HideInInspector]
	public bool startEngineInput;
	
	// Cached rigidbody
	private Rigidbody body;	
	
	// cached references
	protected Drivetrain drivetrain;
	//private CarDynamics cardynamics;
//	private Axles axles;
	
	// car's speed
	private float velo;
	float veloKmh;
	
	public bool smoothInput=true;
	
	// How long it takes to fully engage the throttle
	public float throttleTime = 0.1f;
	// How long it takes to fully release the throttle
	public float throttleReleaseTime = 0.1f;
	
	//maximum allowed throttle in reverse
	public float maxThrottleInReverse=1f;
	
	// How long it takes to fully engage the brakes
	public float brakesTime = 0.1f;
	// How long it takes to fully release the brakes
	public float brakesReleaseTime = 0.1f;
	
	// How long it takes to fully turn the steering wheel from center to full lock
	public float steerTime = 0.1f;
	// How long it takes to fully turn the steering wheel from full lock to center
	public float steerReleaseTime = 0.1f;
	
	// This is added to steerTime per m/s of velocity, so steering is slower when the car is moving faster.
	public float veloSteerTime = 0.05f;	
	// This is added to steerReleaseTime per m/s of velocity, so steering is slower when the car is moving faster.
	public float veloSteerReleaseTime = 0.05f;
	// When detecting a situation where the player tries to counter steer to correct an oversteer situation,
	// steering speed will be multiplied by the difference between steerInput and current steering times this 
	// factor, to make the correction easier.
	public float steerCorrectionFactor = 0f;
	
	// limits max steer angle
	public bool steerAssistance=true;
	public float SteerAssistanceMinVelocity=20;
	
	// Traction control
	public bool TCS = true;
	[HideInInspector]
	public bool TCSTriggered=false;
	public float TCSThreshold =0f;
	public float TCSMinVelocity=20;
	[HideInInspector]
	public float externalTCSThreshold;  // used to improve TCS behaviour with powerMultiplier>1
	
	// ABS
	public bool ABS = true;
	[HideInInspector]
	public bool ABSTriggered=false;
	public float ABSThreshold =0f;
	public float ABSMinVelocity=20;	
	
	//ESP
	public bool ESP=true;
	[HideInInspector]
	public bool ESPTriggered=false;
	public float ESPStrength = 2f;
	public float ESPMinVelocity=35;
	
	[HideInInspector]
	public bool reverse=false;
	
	// Abstract method
	protected abstract void GetInput(out float throttleInput, 
	                                 out float brakeInput, 
	                                 out float steerInput, 
	                                 out float handbrakeInput,
	                                 out float clutchInput,
	                                 out bool startEngineInput,
	                                 out int targetGear);

	protected void BaseTankControllerBegin() {
		BaseTankModBegin();
	}


	void Start () {
		body=rigidbody;
		//cardynamics = GetComponent<CarDynamics>();
		drivetrain = GetComponent<Drivetrain>();
		//axles = GetComponent <Axles>();

		myTransform=transform;
	}

	void Update(){
		int targetGear;
		
		GetInput(out throttleInput, out brakeInput, out steerInput, out handbrakeInput, out clutchInput, out startEngineInput, out targetGear);
		
		if (!drivetrain.changingGear && targetGear != drivetrain.gear){
			drivetrain.Shift(targetGear);
		} 
		
		if (drivetrain.automatic && drivetrain.autoReverse==true){
			if (brakeInput>0 && (velo<=0.5f )){
				reverse=true;
				if (drivetrain.gear!=drivetrain.firstReverse) drivetrain.Shift(drivetrain.firstReverse);
			}
			
			if (throttleInput>0 && (velo<=0.5f )){
				reverse=false;	
				if (drivetrain.gear!=drivetrain.first) drivetrain.Shift(drivetrain.first);
			}
			
			if (reverse==true){
				float temp = throttleInput;
				throttleInput = brakeInput;
				brakeInput = temp;
			} 
		}
		else{
			reverse=false;
		}
		
		brakeKey = brakeInput>0;
		accelKey = throttleInput>0;
	}
	
	void FixedUpdate(){
		maxThrottle=1;
		oldSteering=steering;
		//velo = cardynamics.velo;
		//veloKmh=cardynamics.velo*3.6f;
		bool onGround=drivetrain.OnGround();
		
		if (smoothInput){
			SmoothSteer();
			smoothThrottle();
			smoothBrakes();
		}
		else{
			steering = steerInput; 
			brake = brakeInput; 
			throttle = throttleInput; 
			if (drivetrain.changingGear && drivetrain.automatic) throttle =0;			
		}

		steerTimer=0;
		maxSteer=1;

		
		
		TCSTriggered=false;
		if (TCS && drivetrain.ratio>0 && drivetrain.clutch.GetClutchPosition() >=0.9f && onGround && throttle>drivetrain.idlethrottle && veloKmh>TCSMinVelocity){ //we enable TCS only for speed > TCSMinVelocity (in km/h)
			DoTCS();
		}
		
		ESPTriggered=false;
		if (ESP && drivetrain.ratio>0 && onGround && veloKmh>ESPMinVelocity) { //we enable ESP only for speed > ESPMinVelocity (in km/h)
			DoESP();
		}
		
		ABSTriggered=false;
		if (ABS && brake>0 && veloKmh > ABSMinVelocity && onGround ){  //we enable ABS only for speed > ABSMinVelocity (in km/h)
			DoABS();
		}
		
		float mmaxThrottle;
		if (drivetrain.gearRatios[drivetrain.gear]>0) 
			mmaxThrottle=maxThrottle;
		else 
			mmaxThrottle=maxThrottleInReverse;
		
		if (drivetrain.revLimiterTriggered) throttle=0;
		else if (drivetrain.revLimiterReleased) throttle=throttleInput;
		else throttle = Mathf.Clamp(throttle,drivetrain.idlethrottle,mmaxThrottle);
		
		brake = Mathf.Clamp01(brake);
		steering=Mathf.Clamp(steering ,-1,1);
		deltaSteering=steering-oldSteering;
		

		drivetrain.throttle = throttle;
		if (drivetrain.clutch!=null) {
			if(clutchInput!=0 || drivetrain.autoClutch==false) drivetrain.clutch.SetClutchPosition(1-clutchInput);
		}
		
		drivetrain.startEngine=startEngineInput;
	}
	

	
	void SmoothSteer(){
		float steerSpeed;
		if (steerInput < steering)
		{
			steerSpeed = (steering>0)?(1/(steerReleaseTime+veloSteerReleaseTime*velo)) : (1/(steerTime+veloSteerTime*velo));
			if (steerInput < 0 && steering>0) steerSpeed*=1+ Mathf.Abs(Mathf.Abs(steering) - Mathf.Abs(steerInput))*steerCorrectionFactor;
			steering -= steerSpeed*Time.deltaTime;
			
			if (steerInput > steering)
				steering = steerInput;
		}
		else if (steerInput > steering)
		{
			steerSpeed = (steering<0)?(1/(steerReleaseTime+veloSteerReleaseTime*velo)) :(1/(steerTime+veloSteerTime*velo));			
			if (steerInput > 0 && steering<0) steerSpeed*=1+ Mathf.Abs(Mathf.Abs(steering) - Mathf.Abs(steerInput))*steerCorrectionFactor;
			steering += steerSpeed*Time.deltaTime;
			
			if (steerInput < steering)
				steering = steerInput; 
		}		
	}
	
	void smoothThrottle(){
		if (throttleInput>0 && (!drivetrain.changingGear || !drivetrain.automatic)){
			if (throttleInput < throttle)
			{
				throttle -= Time.deltaTime/throttleReleaseTime;
				
				if (throttleInput > throttle)
					throttle = throttleInput;				
			}
			else if (throttleInput > throttle)
			{			
				throttle += Time.deltaTime/throttleTime;
				
				if (throttleInput < throttle)
					throttle = throttleInput; 
			}
		}
		else {
			throttle -= Time.deltaTime/throttleReleaseTime;
		}
	}
	
	void smoothBrakes(){
		if (brakeInput>0){
			if (brakeInput < brake)
			{
				brake -= Time.deltaTime/brakesReleaseTime;
				
				if (brakeInput > brake)
					brake = brakeInput;				
			}
			else if (brakeInput > brake)
			{			
				brake += Time.deltaTime/brakesTime;
				
				if (brakeInput < brake)
					brake = brakeInput; 
			}
		}
		else {
			brake -= Time.deltaTime/brakesReleaseTime;
		}
	}
	
	void DoABS(){

	}
	
	void DoTCS(){
		float maxSlip=0;

	}
	
	void DoESP(){
		Vector3 driveDir = myTransform.forward;
		Vector3 veloDir = body.velocity;
		veloDir -= myTransform.up*Vector3.Dot(veloDir, myTransform.up);
		veloDir.Normalize();
		
		float angle = 0;
		if (velo>1) 
			angle=-Mathf.Asin(Vector3.Dot(Vector3.Cross(driveDir, veloDir), myTransform.up));
		
		ESPTriggered=false;
		if (angle>0.1f) {//turning right and fishtailing

		}
		else if (angle<-0.1f) {//turning left and fishtailing
		
		}
	}

}
	
