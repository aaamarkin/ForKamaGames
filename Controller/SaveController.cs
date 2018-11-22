using System;
using System.Collections.Generic;
using System.Linq; using System.Text;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveController : AppElement
{

	private byte[] IV = {0x6c, 0x1e, 0x85, 0x5e, 0x97, 0x4a, 0x9e, 0x39, 0x9b, 0x80, 0x33, 0x31, 0x5d, 0x76, 0x6e, 0xc5};
	private byte[] Key = {0x78, 0x06, 0x2f, 0x16, 0x3a, 0x5f, 0x4d, 0xcc, 0xe4, 0x28, 0xb7, 0x6f, 0x75, 0x1b, 0xd4, 0xb8};

	private string filename = "/save.data";

	public override void LastInAwake()
	{
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	public void Save(int maxScore)
	{
		if (app.Model.SaveModel.SaveOnPhone)
		{
			SaveOnPhone(maxScore);
		}
		if (app.Model.SaveModel.SaveOnPlayfab)
		{
			SaveOnPlayfab(maxScore);
		}
		
	}

	public void Load()
	{
		LoadFromPlayfab();
	}

	private string GetPathBasedOnOS()
	{
		if (Application.isEditor)
			return "file://" + Application.persistentDataPath + "/" + filename;
		else if (Application.isMobilePlatform || Application.isConsolePlatform)
			return Application.persistentDataPath + filename;
		else // For standalone player.
			return "file://" + Application.persistentDataPath + "/" + filename;
	}

	private void SaveOnPhone(int maxScore)
	{
		FileStream fileStream = new FileStream(GetPathBasedOnOS(), FileMode.Create);
		RijndaelManaged RMCrypto = new RijndaelManaged();
		CryptoStream CryptStream = new CryptoStream(fileStream, RMCrypto.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
		BinaryWriter writer = new BinaryWriter(CryptStream);
		writer.Write(maxScore);
		writer.Close();
		CryptStream.Close();
		fileStream.Close();
	}

	public int LoadFromPhone()
	{
		int maxScore = 0;

		if (!File.Exists(GetPathBasedOnOS()))
			return maxScore;
		try
		{
			FileStream fileStream = new FileStream(GetPathBasedOnOS(), FileMode.Open);
			RijndaelManaged RMCrypto = new RijndaelManaged();
			CryptoStream CryptStream = new CryptoStream(fileStream, RMCrypto.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
			BinaryReader reader = new BinaryReader(CryptStream);
			maxScore = reader.ReadInt32();
			reader.Close();
			CryptStream.Close();
			fileStream.Close();

			app.Controller.GameSceneController.SetMaxScore(maxScore);

			return maxScore;

		}
		catch (Exception e)
		{
			return maxScore;
		}
	}
	
	

	private void SaveOnPlayfab(int maxScore)
	{
		app.Controller.PlayfabController.Save(maxScore);
	}

	private void LoadFromPlayfab()
	{
		app.Controller.PlayfabController.Load();
//		print("LoadFromPlayfab() = " + l);
//		return l;
	}
}
