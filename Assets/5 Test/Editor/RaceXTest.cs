﻿using System;
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
			serializerEntity = new JSONSerializer();
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
		public void SerializeEmptyGameData() {
			string serializedGame = serializerEntity.SerializeToString(gameEntity);			
			Assert.True(serializedGame == "{\"gameData\":{\"timePlayingTotal\":0,\"timePlayingThisSession\":0,\"playerName\":\"\"}}");
		}

		[Test]
		public void SerializeGameDataWithTestName() {
			gameEntity.SetPlayerName("Test");
			string serializedGame = serializerEntity.SerializeToString(gameEntity);
			Assert.True(serializedGame == "{\"gameData\":{\"timePlayingTotal\":0,\"timePlayingThisSession\":0,\"playerName\":\"Test\"}}");
		}

		[Test]
		public void SerializeAndDeserializeGameDataWithTestName() {
			gameEntity.SetPlayerName("Test");
			string serializedGame = serializerEntity.SerializeToString(gameEntity);
			gameEntity.SetPlayerName("");
			gameEntity = serializerEntity.DeserializeFromString<IGameEntity>(serializedGame);
			Assert.True(serializedGame == "{\"gameData\":{\"timePlayingTotal\":0,\"timePlayingThisSession\":0,\"playerName\":\"Test\"}}");
		}

	}
}
