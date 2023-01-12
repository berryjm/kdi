using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizSceneHearts : MonoBehaviour
{
	public CheckboxData checkboxData;
	public Button button1;
	public Button button2;
	public Button button3;
	public Text mainText;
	public Text debugText;
    public GameObject heartPrefab;
	public int heart = 3;
	//Arrays to store the lines from the text files
	private List<string> kanji = new List<string>();
	private List<string> english = new List<string>();
	private List<string> kunon = new List<string>();
	private SpriteRenderer[] hearts;
	private int randomIndex;
	private string realAnswer;
    private byte[] encryptionKey;
    private byte[] encryptionIV;
private void Start()
{
    if (PlayerPrefs.HasKey("encryptionKey") && PlayerPrefs.HasKey("encryptionIV"))
        {
        string keyString = PlayerPrefs.GetString("encryptionKey");
        string IVString = PlayerPrefs.GetString("encryptionIV");
        encryptionKey = Convert.FromBase64String(keyString);
        encryptionIV = Convert.FromBase64String(IVString);
        }
        else
        {
            encryptionKey = GenerateEncryptionKey();
            encryptionIV = GenerateEncryptionIV();
        }
		hearts = new SpriteRenderer[3];
		for (int i = 0; i < hearts.Length; i++)
		{
			hearts[i] = Instantiate(heartPrefab, new Vector3(i * 1.5f, 0, 0), Quaternion.identity).GetComponent<SpriteRenderer>();
		}
		heart = DecryptHeartCount();
		UpdateHearts();
		LoadLessons();
		button1.onClick.AddListener(() => CheckAnswer(button1.GetComponentInChildren<Text>().text));
		button2.onClick.AddListener(() => CheckAnswer(button2.GetComponentInChildren<Text>().text));
		button3.onClick.AddListener(() => CheckAnswer(button3.GetComponentInChildren<Text>().text));
		GetQuestion();
	}

	// ...
	private void LoadLessons()
	{
		// Loop through the checkboxes in checkboxData
		for (int i = 0; i < checkboxData.checkboxStates.Length; i++)
		{
			if (checkboxData.checkboxStates[i])
			{
				// If the checkbox at index i is checked, load the corresponding lesson files
				// Load Kanji file
				TextAsset kanjiTextAsset = Resources.Load<TextAsset>("Quiz/" + (i + 1) + "/kanji");
				string[] kanjiLines = kanjiTextAsset.text.Split("\n"[0]);
				// Load English file
				TextAsset englishTextAsset = Resources.Load<TextAsset>("Quiz/" + (i + 1) + "/english");
				string[] englishLines = englishTextAsset.text.Split("\n"[0]);
				// Load kunon file
				TextAsset kunonTextAsset = Resources.Load<TextAsset>("Quiz/" + (i + 1) + "/kunon");
				string[] kunonLines = kunonTextAsset.text.Split("\n"[0]);
				kanji.AddRange(kanjiLines);
				english.AddRange(englishLines);
				kunon.AddRange(kunonLines);
			}
		}
	}

	public void GetQuestion()
	{
    if (heart <= 0)
    {
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        return;
    }
    else 
    {
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
    }

		//Get a random line from the english pool
		randomIndex = UnityEngine.Random.Range(0, english.Count);
		string englishWord = english[randomIndex];
		//Print the question
		mainText.text = "What is the kanji for " + englishWord + "?";
		//Populate the buttons with the possible answers
		//Real answer
		realAnswer = kanji[randomIndex];
		//Fake answers
		string fakeAnswer1 = kanji[(randomIndex + 1) % kanji.Count];
		string fakeAnswer2 = kanji[(randomIndex + 2) % kanji.Count];
		//Create a list of the answers
		List<string> answers = new List<string>()
		{realAnswer, fakeAnswer1, fakeAnswer2};
		//Shuffle the list of answers
		for (int i = 0; i < answers.Count; i++)
		{
			int randomIndex2 = UnityEngine.Random.Range(i, answers.Count);
			string temp = answers[i];
			answers[i] = answers[randomIndex2];
			answers[randomIndex2] = temp;
		}

		// Assign the answers to the buttons
		button1.GetComponentInChildren<Text>().text = answers[0];
		button2.GetComponentInChildren<Text>().text = answers[1];
		button3.GetComponentInChildren<Text>().text = answers[2];
	}

	private void CheckAnswer(string answer)
	{
		if (answer == realAnswer)
		{
			//Correct answer
			//Code for correct answer
			GetQuestion();
		}
		else
		{
			//Incorrect answer
			heart--;
			UpdateHearts();
			//if (heart <= 0)
			//{
				// play Ad and give heart
			//	heart++;
		//		UpdateHearts();
		//	}
		}

		EncryptHeartCount();
	}

	private void UpdateHearts()
	{
		for (int i = 0; i < hearts.Length; i++)
		{
			if (i < heart)
			{
				hearts[i].GetComponent<SpriteRenderer>().enabled = true;
			}
			else
			{
				hearts[i].GetComponent<SpriteRenderer>().enabled = false;
			}
		}

		if (heart <= 0)
		{
			button1.interactable = false;
			button2.interactable = false;
			button3.interactable = false;
		}
	}

    private byte[] GenerateEncryptionKey()
{
    using (var rijndael = new RijndaelManaged())
    {
        rijndael.KeySize = 256;
        rijndael.GenerateKey();
        return rijndael.Key;
    }
}

private byte[] GenerateEncryptionIV()
{
    using (var rijndael = new RijndaelManaged())
    {
        rijndael.GenerateIV();
        return rijndael.IV;
    }
}

private void EncryptHeartCount()
{
    RijndaelManaged rijndael = new RijndaelManaged();
    rijndael.Key = encryptionKey;
    rijndael.IV = encryptionIV;

    ICryptoTransform encryptor = rijndael.CreateEncryptor();

    MemoryStream encryptMemoryStream = new MemoryStream();
    CryptoStream cryptoStream = new CryptoStream(encryptMemoryStream, encryptor, CryptoStreamMode.Write);
    Debug.Log("heartCount:" + heart);
    byte[] plainText = BitConverter.GetBytes(heart);
    cryptoStream.Write(plainText, 0, plainText.Length);

    cryptoStream.FlushFinalBlock();
    byte[] encryptedData = encryptMemoryStream.ToArray();

    File.WriteAllBytes(Application.persistentDataPath + "/heartcount.dat", encryptedData);
    cryptoStream.Close();
    encryptMemoryStream.Close();
    PlayerPrefs.SetString("encryptionKey", Convert.ToBase64String(encryptionKey));
    PlayerPrefs.SetString("encryptionIV", Convert.ToBase64String(encryptionIV));
}

private int DecryptHeartCount()
{
    // Create a byte array to hold the decrypted data
    byte[] decryptedHeartCountBytes;

    // Check if the heart count file exists
    if (File.Exists(Application.persistentDataPath + "/heartcount.dat"))
    {
        // Read the encrypted data from the file
        byte[] encryptedHeartCountBytes = File.ReadAllBytes(Application.persistentDataPath + "/heartcount.dat");

        // Create a new RijndaelManaged object for decryption
        using (var rijndaelManaged = new RijndaelManaged())
        {
            // Set the key and IV for the RijndaelManaged object
            rijndaelManaged.Key = encryptionKey;
            rijndaelManaged.IV = encryptionIV;

            // Create a new CryptoStream object for decryption
            using (var cryptoStream = new CryptoStream(new MemoryStream(encryptedHeartCountBytes), rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Read))
            {
                // Create a new MemoryStream to hold the decrypted data
                using (var memoryStream = new MemoryStream())
                {
                    // Read the decrypted data into the MemoryStream
                    cryptoStream.CopyTo(memoryStream);
                    decryptedHeartCountBytes = memoryStream.ToArray();
                }
            }
        }
    }
    else
    {
        // If the file doesn't exist, return the default heart count
        if (!PlayerPrefs.HasKey("encryptionKey") && !PlayerPrefs.HasKey("encryptionIV")) {
            return 3;
        }
        else {
            return 0;
        }
    }

    // Convert the decrypted byte array to an integer and return it
    return BitConverter.ToInt32(decryptedHeartCountBytes, 0);
}


    /* 
	private byte[] GenerateEncryptionKey()
	{
		// Generate a unique encryption key
		// You can replace this with a manually generated key
		var aes = Aes.Create();
		return aes.Key;
	}

	private byte[] GenerateEncryptionIV()
	{
		// Generate a unique encryption IV
		// You can replace this with a manually generated IV
		var aes = Aes.Create();
		return aes.IV;
	}

	private void EncryptHeartCount()
	{
		// Convert the heart count to a byte array
		byte[] heartCountBytes = BitConverter.GetBytes(heart);
		// Encrypt the heart count
		byte[] encryptedHeartCount;
		using (Aes aes = Aes.Create())
		{
			aes.Key = encryptionKey;
			aes.IV = encryptionIV;
			ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
			using (MemoryStream msEncrypt = new MemoryStream())
			{
				using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				{
					csEncrypt.Write(heartCountBytes, 0, heartCountBytes.Length);
				}

				encryptedHeartCount = msEncrypt.ToArray();
			}
		}

		// Save the encrypted heart count to a file
		File.WriteAllBytes("heartcount.dat", encryptedHeartCount);
	}

	private int DecryptHeartCount()
	{
		int decryptedHeartCount = 3;
		// Check if the heart count file exists
		if (File.Exists("heartcount.dat"))
		{
            Debug.Log("Encryption key: " + Convert.ToBase64String(encryptionKey));
            Debug.Log("Encryption IV: " + Convert.ToBase64String(encryptionIV));

			// Read the encrypted heart count from the file
			byte[] encryptedHeartCount = File.ReadAllBytes("heartcount.dat");
			// Decrypt the heart count
			byte[] decryptedHeartCountBytes;
			using (Aes aes = Aes.Create())
			{
				aes.Key = encryptionKey;
				aes.IV = encryptionIV;
				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
				using (MemoryStream msDecrypt = new MemoryStream(encryptedHeartCount))
				{
                    try {
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						decryptedHeartCountBytes = new byte[sizeof(int)];
						csDecrypt.Read(decryptedHeartCountBytes, 0, decryptedHeartCountBytes.Length);
					}}
                    catch (CryptographicException e)
{
    Debug.LogError("Cryptographic exception: " + e.Message);
    return 0;
}

				}
			}

			// Convert the decrypted bytes to an int
			decryptedHeartCount = BitConverter.ToInt32(decryptedHeartCountBytes, 0);
		}

		return decryptedHeartCount;
	}*/
}