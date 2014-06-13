# Test Structure
* Test is the most important part while developing to ensure refactoring is not causing additional issues.

## Notes
* Always create tests for each use case.
[Test]
		//[Ignore ("Ignored test")] //skip this test
		//[MaxTime (100)] //maximum time a test can run before failing
		//[Category ("Example  Test")]
		//[ExpectedException (typeof (ArgumentException), ExpectedMessage = "expected message")]
		//public void ExampleTest ()
		//public void ParameterizedTest ([Values (1, 2, 3)] int a)
		//public void RangeTest ( [Range (1, 10, 3)] int x )
		//{
		//	throw new Exception ("Exception throwing test");
		//	Assert.Fail ();
		//	Assert.Pass ();
		//	Assert.Inconclusive();

		//}
		/*[Datapoint]
		public double zero = 0;
		[Datapoint]
		public double positive = 1;
		[Datapoint]
		public double negative = -1;
		[Datapoint]
		public double max = double.MaxValue;
		[Datapoint]
		public double infinity = double.PositiveInfinity;
		
		[Theory]
		public void SquareRootDefinition ( double num )
		{
			Assume.That (num >= 0.0 && num < double.MaxValue);
			
			var sqrt = Math.Sqrt (num);
			
			Assert.That (sqrt >= 0.0);
			Assert.That (sqrt * sqrt, Is.EqualTo (num).Within (0.000001));
		}
		*/