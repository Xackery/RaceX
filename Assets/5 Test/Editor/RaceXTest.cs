using System;
using System.Threading;
using NUnit.Framework;

namespace UnityTest
{

	[TestFixture]
	[Category ("Game Start Tests")]
	internal class RaceXTest  {
		IGameEntity gameEntity;
		ISerializerEntity serializerEntity;
		ILoggerEntity loggerEntity;

		[Test]
		public void CreateGame() {
			gameEntity = new RaceXEntity();
			Assert.IsNotNull(gameEntity);
		}

		[Test]
		public void CreateSerializer() {
			serializerEntity = new JSONSerializerEntity();
			Assert.IsNotNull(serializerEntity);
		}

		[Test]
		public void CreateLogger() {
			loggerEntity = new DebugLoggerEntity();
			Assert.IsNotNull(loggerEntity);
		}

		[Test]
		public void PlayerNameEmpty() {
			gameEntity.SetPlayerName("");
			Assert.True (gameEntity.GetPlayerName() == "");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void PlayerNameTooBig() {
			gameEntity.SetPlayerName("asjifaosjifasfjaosifjasiasjifaosjifasfjaosifjasiasjifaosjifasfjaosifjasiasjifaosjifasfjaosifjasiasjifaosjifasfjaosifjasiasdasdfg");
		}

		[Test]
		public void SetGameScore() {
			gameEntity = new RaceXEntity();
			gameEntity.SetGameScore(0);
			Assert.True(gameEntity.GetGameScore() == 0);
		}

		[Test]
		public void AddGameScore() {
			gameEntity = new RaceXEntity();
			gameEntity.SetGameScore(1);
			gameEntity.AddGameScore(1);
			Assert.True(gameEntity.GetGameScore() == 2);
		}

		[Test]
		public void SubtractGameScore() {
			gameEntity = new RaceXEntity();
			gameEntity.SetGameScore(1);
			gameEntity.SubtractGameScore(1);
			Assert.True(gameEntity.GetGameScore() == 0);
		}

		[Test]
		public void SerializeEmptyGameData() {
			gameEntity = new RaceXEntity();
			string serializedGame = serializerEntity.SerializeToString(gameEntity.GetGameData());
			Assert.True(serializedGame == "{\"timePlayingTotal\":0,\"timePlayingThisSession\":0,\"playerName\":\"\",\"gameScore\":0}");
		}

		[Test]
		public void SerializeGameDataWithTestName() {
			gameEntity.SetPlayerName("Test");
			string serializedGame = serializerEntity.SerializeToString(gameEntity.GetGameData());
			Assert.True(serializedGame == "{\"timePlayingTotal\":0,\"timePlayingThisSession\":0,\"playerName\":\"Test\",\"gameScore\":0}");
		}

		[Test]
		public void SerializeAndDeserializeGameDataWithTestName() {
			gameEntity.SetPlayerName("Test");
			string serializedGame = serializerEntity.SerializeToString(gameEntity.GetGameData());
			gameEntity.SetPlayerName("");
			gameEntity.SetGameData(serializerEntity.DeserializeFromString<RaceXData>(serializedGame));
			Assert.True(serializedGame == "{\"timePlayingTotal\":0,\"timePlayingThisSession\":0,\"playerName\":\"Test\",\"gameScore\":0}");
		}




	}
}
