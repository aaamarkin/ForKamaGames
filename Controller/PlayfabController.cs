using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.AdminModels;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.iOS;
using System.Collections;

public class PlayfabController : AppElement {

	public void Start()
	{
		PlayFabSettings.TitleId = "1108"; // Please change this value to your own titleId from PlayFab Game Manager

		#if !UNITY_EDITOR
        var request = new PlayFab.ClientModels.LoginWithIOSDeviceIDRequest{ DeviceId = Device.vendorIdentifier, CreateAccount = true}; 
		
		PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginSuccess, OnLoginFailure);
	
		#else   
		
        var request = new PlayFab.ClientModels.LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
		PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);


        #endif


	}

	private void OnLoginSuccess(LoginResult result)
	{
		Debug.Log("Congratulations, you made your first successful API call!");

	}

	private void OnLoginFailure(PlayFabError error)
	{
		Debug.LogWarning("Something went wrong with your first API call.  :(");
		Debug.LogError("Here's some debug information:");
		Debug.LogError(error.GenerateErrorReport());
	}

	public void Save(int maxScore)
	{
		try

		{
            PlayFabClientAPI.UpdateUserData(new PlayFab.ClientModels.UpdateUserDataRequest() {
					Data = new Dictionary<string, string>() {
						{"MaxScore", maxScore.ToString()}
					}
				}, 
				result => Debug.Log("Successfully updated user data"),
				error => {
					Debug.Log("Got error setting user data MaxScore to " + maxScore);
					Debug.Log(error.GenerateErrorReport());
				});
		}
		catch (Exception e)
		{
//				Console.WriteLine(e);
//				throw;
//			Debug.Log("Got error retrieving user data:");
		}
		
	}
	
	public void Load()
	{
		int maxScore = 0;
		
		if (app.Model.SaveModel.SaveOnPhone)
		{
			maxScore = app.Controller.SaveController.LoadFromPhone();
		}

		if (app.Model.SaveModel.SaveOnPlayfab)
		{
			try
			
			{
                PlayFabClientAPI.GetUserData(new PlayFab.ClientModels.GetUserDataRequest() {
					Keys = new List<string> {"MaxScore"}
				}, result => {
					Debug.Log("Got user data:");
					if (result.Data == null || !result.Data.ContainsKey("MaxScore")) Debug.Log("No MaxScore");
					else
					{
//				maxScore = 
				
//				app.Model.GameSceneModel.MaxScore = Convert.ToInt32(result.Data["MaxScore"].Value);
				
				if (maxScore < Convert.ToInt32(result.Data["MaxScore"].Value)){
					
					app.Controller.GameSceneController.SetMaxScore(Convert.ToInt32(result.Data["MaxScore"].Value));	
					
				}
				
				
//				print("maxScore = " + maxScore);
//				print("result.Data[MaxScore].Value = " + result.Data["MaxScore"].Value);
//				print("Convert.ToInt32(result.Data[MaxScore].Value, 16) = " + Convert.ToInt32(result.Data["MaxScore"].Value));
					}
				}, (error) => {
				
//				app.Controller.SaveController.LoadFromPhone();
					Debug.Log("Got error retrieving user data:");
					Debug.Log(error.GenerateErrorReport());
				});
			}
			
			catch (Exception e)
			{
//				Console.WriteLine(e);
//				throw;
				Debug.Log("Got error retrieving user data:");
				app.Controller.SaveController.LoadFromPhone();
			}
			
		}



	}

    public string GetContentUploadURL()
    {
        string uploadUrl = "";

        try
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("X-SecretKey", "PEIOF38Y9KZ5UEHT341Z3ZTASM9M33BDWKAXKOZOJTXHUGFKST");
            PlayFabAdminAPI.GetContentUploadUrl(new PlayFab.AdminModels.GetContentUploadUrlRequest()
            {
                Key = "images/sword_icon2"
            }, result => {
                Debug.Log("Got upload URL:");
                if (result.URL == null) Debug.Log("No upload URL");
                else
                {

                    uploadUrl = result.URL;
                    StartCoroutine("UploadContent", uploadUrl);
 
                }
            }, (error) => {

                //              app.Controller.SaveController.LoadFromPhone();
                Debug.Log("Got error retrieving upload URL:");
                Debug.Log(error.GenerateErrorReport());
            }, null, headers);
        }
        catch (Exception e)
        {
            //              Console.WriteLine(e);
            //              throw;
            Debug.Log("Got error retrieving upload URL:");
        }

        return uploadUrl;
    }

    public string GetContentDownloadURL()
    {
        string downloadUrl = "";

        try
        {
            //Dictionary<string, string> headers = new Dictionary<string, string>();
            //headers.Add("X-SecretKey", "PEIOF38Y9KZ5UEHT341Z3ZTASM9M33BDWKAXKOZOJTXHUGFKST");

            PlayFabClientAPI.GetContentDownloadUrl(new PlayFab.ClientModels.GetContentDownloadUrlRequest()
            {
                Key = "images/sword_icon2",
                ThruCDN = false
            }, result => {
                Debug.Log("Got download URL:");
                if (result.URL == null) Debug.Log("No download URL");
                else
                {

                    downloadUrl = result.URL;
                    StartCoroutine("DownloadContent", downloadUrl);

                }
            }, (error) => {

                //              app.Controller.SaveController.LoadFromPhone();
                Debug.Log("Got error retrieving download URL:");
                Debug.Log(error.GenerateErrorReport());
            });
        }
        catch (Exception e)
        {
            //              Console.WriteLine(e);
            //              throw;
            Debug.Log("Got error retrieving download URL:");
        }

        return downloadUrl;
    }

    //public void GetContentUploadURL(){

    //    string postData = "{\"Key\": \"images/sword_icon.png\",  \"ContentType\": \"application/octet-stream\"}";
    //    string urlStr = "https://1108.playfabapi.com/Admin/GetContentUploadUrsl";

    //    UnityWebRequest www = new UnityWebRequest(); // Completely blank

    //    www.url = urlStr;
    //    www.method = UnityWebRequest.kHttpVerbPOST; 

    //    www.SetRequestHeader("Content-Type", "application/json");
    //    www.SetRequestHeader("X-SecretKey", "PEIOF38Y9KZ5UEHT341Z3ZTASM9M33BDWKAXKOZOJTXHUGFKST");

    //    www.useHttpContinue = false;
    //    www.chunkedTransfer = false;
    //    www.redirectLimit = 0;  // disable redirects
    //    www.timeout = 60;       // don't make this small, web requests do take some time

    //    UnityWebRequestAsyncOperation operation = www.SendWebRequest();

    //    if (www.isNetworkError || www.isHttpError)
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        Debug.Log("GetContentUploadURL complete!");

    //        Debug.Log(www.downloadHandler.text);
    //    }

    //    if (operation.isDone){
            
    //    }

    //}

    //public void GetContentDownloadURL(){
        
    //}

    IEnumerator UploadContent(string url){

        print("Uploading content to " + url);

        //byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");

        //int[] my = new int[10];

        //for (int i = 0; i < 10; i++){
        //    my[i] = i + 3;
        //}

        //Dictionary<int, Tuple<Vector3, bool>> trailPositions;

        byte[] myPositions = new byte[(app.Model.GameSceneModel.LineRendererRowModel.trailPositions.Count / 10 ) * 2 * 2 + (app.Model.GameSceneModel.LineRendererRowModel.trailPositions.Count / 10) + 2 + 10];

        int myIncrement = 0;

        short frameLength = (short) (app.Model.GameSceneModel.LineRendererRowModel.trailPositions.Count / 10);

        byte[] frameLengthBytes = WrightByte(frameLength);

        for (int j = 0; j < 2; j++)
        {
            myPositions[j] = frameLengthBytes[j];

            myIncrement += 1;
        }

        for (int i = 0; i < app.Model.GameSceneModel.LineRendererRowModel.trailPositions.Count; i += 10)
        {

            short xPosition = (short) Mathf.RoundToInt(app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].first.x * 1000);

            short yPosition = (short) Mathf.RoundToInt(app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].first.y * 1000);

            byte isPainting = Convert.ToByte(app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].second);

            byte[] xPositionBytes = WrightByte(xPosition);

            byte[] yPositionBytes = WrightByte(yPosition);

            for (int j = 0; j < 2; j++)
            {
                myPositions[myIncrement] = xPositionBytes[j];

                myIncrement += 1;
            }

            for (int j = 0; j < 2; j++)
            {
                myPositions[myIncrement] = yPositionBytes[j];

                myIncrement += 1;
            }

            myPositions[myIncrement] = isPainting;

            Debug.Log("myIncrement = " + myIncrement);

            Debug.Log("xPositionBytes = " + String.Join(" ", new List<byte>(xPositionBytes).ConvertAll(k => k.ToString()).ToArray()));

            Debug.Log("yPositionBytes = " + String.Join(" ", new List<byte>(yPositionBytes).ConvertAll(k => k.ToString()).ToArray()));

            Debug.Log("tmpVec.x = " + xPosition);

            Debug.Log("tmpVec.y = " + yPosition);

            Debug.Log("isPainting = " + app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].second);

            myIncrement += 1;

        }

        //byte[] result = new byte[my.Length * 4];

        //for (int i = 0; i < my.Length; i++){
        //    int q = my[i];

        //    byte[] intBytes = WrightByte(q);

        //    for (int j = 0; j < 4; j++){
        //        result[i * 4 + j] = intBytes[j];
        //    }
        //}
        Debug.Log("myPositions = " + String.Join(" ", new List<byte>(myPositions).ConvertAll(k => k.ToString()).ToArray()));

        UnityWebRequest www = UnityWebRequest.Put(url, myPositions);

        www.SetRequestHeader("Content-Type", "binary/octet-stream");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            //Debug.Log(String.Join(" ", new List<int>(my).ConvertAll(i => i.ToString()).ToArray()));

            //Debug.Log(String.Join(" ", new List<byte>(result).ConvertAll(i => i.ToString()).ToArray()));

        }
    }

    IEnumerator DownloadContent(string url)
    {

        print("Downloading content from " + url);



        UnityWebRequest www = UnityWebRequest.Get(url);

        //www.SetRequestHeader("Content-Type", "binary/octet-stream");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            
        }
        else
        {
            
            Debug.Log("Download complete!");

            // Show results as text
            //Debug.Log(www.downloadHandler.text);

            //        // Or retrieve results as binary data

            Dictionary<int, Tuple<Vector3, bool>> tmpTrailPositions = new Dictionary<int, Tuple<Vector3, bool>>();

            Vector3 tmpVec = new Vector3();

            tmpVec.z = app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position.z;
               
            byte[] byteResults = www.downloadHandler.data;

            Debug.Log("byteResults = " + String.Join(" ", new List<byte>(byteResults).ConvertAll(k => k.ToString()).ToArray()));

            byte[] tmp = new byte[2];

            for (int j = 0; j < 2; j++)
            {

                tmp[j] = byteResults[j];

            }

            short frameLength = WrightInt(tmp);

            int myIncrement = 0;

            for (int i = 2; i < byteResults.Length; i += 5){

                for (int j = 0; j < 2; j++){

                    tmp[j] = byteResults[i + j];
                        
                }

                short xPosition = WrightInt(tmp);

                Debug.Log("xPositionByte = " + String.Join(" ", new List<byte>(tmp).ConvertAll(k => k.ToString()).ToArray()));

                for (int j = 0; j < 2; j++)
                {

                    tmp[j] = byteResults[i + j + 2];

                }

                short yPosition = WrightInt(tmp);

                Debug.Log("yPositionByte = " + String.Join(" ", new List<byte>(tmp).ConvertAll(k => k.ToString()).ToArray()));

                bool isPainting = Convert.ToBoolean(byteResults[i - 1]);

                tmpVec.x = xPosition / 1000f;

                tmpVec.y = yPosition / 1000f;

                Debug.Log("myIncrement = " + myIncrement);

                Debug.Log("xPosition = " + xPosition);

                Debug.Log("yPosition = " + yPosition);

                Debug.Log("tmpVec.x = " + tmpVec.x);

                Debug.Log("tmpVec.y = " + tmpVec.y);

                Debug.Log("isPainting = " + isPainting);

                tmpTrailPositions.Add(myIncrement, new Tuple<Vector3, bool>(tmpVec, isPainting));

                myIncrement += 1;

            }



            //int[] intResult = new int[byteResults.Length / 4];

            //for (int i = 0; i < intResult.Length; i++)
            //{

            //    byte[] tmp = new byte[4];

            //    for (int j = 0; j < 4; j++)
            //    {
            //        tmp[j] = byteResults[i * 4 + j];
            //    }

            //    if (BitConverter.IsLittleEndian)
            //        Array.Reverse(tmp);

            //    int q = BitConverter.ToInt32(tmp, 0);

            //    intResult[i] = q;
            //}



            app.Model.GameSceneModel.LineRendererRowModel.trailPositions = tmpTrailPositions;


            app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position =
            app.Model.GameSceneModel.LineRendererRowModel.trailPositions[0].first;

            app.Controller.GameSceneController.LineRendererRowController.SetTrailPaintingOn();

            Vector3 previousPos = app.Model.GameSceneModel.LineRendererRowModel.trailPositions[0].first;

            int compressCounter = 0;

            for (int i = 1; i < frameLength; i+=1)
            {
                if (Mathf.Abs(previousPos.x - app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].first.x) > 0.001 &&
                    Mathf.Abs(previousPos.y - app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].first.y) > 0.001)
                {

                    yield return new WaitForSeconds(0.1f);

                    app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position =
                        app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].first;

                    if (app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].second)
                    {
                        app.Controller.GameSceneController.LineRendererRowController.SetTrailPaintingOn();

                    }
                    else
                    {
                        app.Controller.GameSceneController.LineRendererRowController.SetTrailPaintingOff();
                    }

                    print("app.View.GameSceneView.LineRendererRowView.LineRendererView.TrailRenderer.gameObject = " + app.View.GameSceneView.LineRendererRowView.LineRendererView.TrailRenderer.gameObject.activeSelf);

                    print("app.View.GameSceneView.LineRendererRowView.LineRendererView.TrailRenderer.gameObject = " + app.View.GameSceneView.LineRendererRowView.LineRendererView.gameObject.activeSelf);

                    print("app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position = " + app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position);

                    print("app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].first = " + app.Model.GameSceneModel.LineRendererRowModel.trailPositions[i].first);

                    compressCounter += 1;
                }

            }

            print("app.Model.GameSceneModel.LineRendererRowModel.trailPositions.Count = " + app.Model.GameSceneModel.LineRendererRowModel.trailPositions.Count);
            print("app.Model.GameSceneModel.LineRendererRowModel.trailPositions.Count/10 = " + app.Model.GameSceneModel.LineRendererRowModel.trailPositions.Count/10);
            print("compressCounter = " + compressCounter);
           

            //Debug.Log(String.Join(" ", new List<byte>(byteResults).ConvertAll(i => i.ToString()).ToArray()));

            //Debug.Log(String.Join(" ", new List<int>(intResult).ConvertAll(i => i.ToString()).ToArray()));

            //int[] my = new int[10];

            //for (int i = 0; i < 10; i++)
            //{
            //    my[i] = i;
            //}

            //byte[] result = new byte[my.Length * sizeof(int)];

            //Buffer.BlockCopy(my, 0, result, 0, result.Length);
        }
    }

    //IEnumerator GetText()
    //{
    //    UnityWebRequest www = UnityWebRequest.Get("http://www.my-server.com");
    //    yield return www.SendWebRequest();

    //    if (www.isNetworkError || www.isHttpError)
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        // Show results as text
    //        Debug.Log(www.downloadHandler.text);

    //        // Or retrieve results as binary data
    //        byte[] results = www.downloadHandler.data;
    //    }
    //}

 
    private byte[] WrightByte(short q)
    {
        byte[] intBytes = BitConverter.GetBytes(q);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(intBytes);
        return intBytes;
    }

    private short WrightInt(byte[] q)
    {
        if (BitConverter.IsLittleEndian)
            Array.Reverse(q);

        return BitConverter.ToInt16(q, 0);
    }
}
