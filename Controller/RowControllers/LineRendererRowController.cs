using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererRowController : AppElement {

	private float time;

	private void Start()
	{
		app.Model.GameSceneModel.LineRendererRowModel.trailPositions = new Dictionary<int, Tuple<Vector3, bool>>();
		
		app.Model.GameSceneModel.LineRendererRowModel.TrailRecordingFrameNumber = 0;

		app.Model.GameSceneModel.LineRendererRowModel.isTrailRecorded = false;
		
		app.Model.GameSceneModel.LineRendererRowModel.isTrailPainting = false;
		
		app.Model.GameSceneModel.LineRendererRowModel.trailPositions[0] =
			new Tuple<Vector3, bool>(app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position, false);
	}

	void FixedUpdate()
	{
		if (app.Model.GameSceneModel.runAllMovingComponents)
		{
			time += Time.fixedDeltaTime;
		
			if (time >= app.Model.GameSceneModel.LineRendererRowModel.instantiationPeriod)
			{
				Instantiate(app.Model.GameSceneModel.LineRendererRowModel.block,
					app.View.GameSceneView.LineRendererRowView.RightBorderView.transform.position,
					app.Model.GameSceneModel.LineRendererRowModel.block.rotation);

				time = 0;
			}
		}

		
	}

	private void Update()
	{
		if (app.Model.GameSceneModel.LineRendererRowModel.isTrailRecorded)
		{

			app.Model.GameSceneModel.LineRendererRowModel.trailPositions
					[app.Model.GameSceneModel.LineRendererRowModel.TrailRecordingFrameNumber] =
				new Tuple<Vector3, bool>(app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position,
					app.Model.GameSceneModel.LineRendererRowModel.isTrailPainting);
				
			app.Model.GameSceneModel.LineRendererRowModel.TrailRecordingFrameNumber += 1;
		}
	}

	public void SetTrailRecodingOn()
	{
		app.Model.GameSceneModel.LineRendererRowModel.isTrailRecorded = true;
	}

	public void SetTrailRecordingOff()
	{
		app.Model.GameSceneModel.LineRendererRowModel.isTrailRecorded = false;
	}
	
	public void SetTrailPaintingOn()
	{
		app.Model.GameSceneModel.LineRendererRowModel.isTrailPainting = true;
		
		app.View.GameSceneView.LineRendererRowView.LineRendererView.TrailRenderer.gameObject.SetActive(true);
	}

	public void SetTrailPaintingOff()
	{
		app.Model.GameSceneModel.LineRendererRowModel.isTrailPainting = false;
		
		app.View.GameSceneView.LineRendererRowView.LineRendererView.TrailRenderer.gameObject.SetActive(false);
	}

	public void ReplayTrail()
	{

		SetTrailRecordingOff();

		SetTrailPaintingOff();

		

		ClearTrail();

        app.Controller.GameSceneController.LineRendererRowController.SetTrailPaintingOn();

        LoadTrail();

        //MoveTrailAlongPath();


		
        //StartCoroutine("TMP");

	}

    IEnumerator TMP()
    {


        //yield return new WaitForSeconds(1);
        yield return true;

        //app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position =
        //app.Model.GameSceneModel.LineRendererRowModel.trailPositions[0].first;



        for (float i = 0; i < 10; i++)
        {
            Vector3 odlPos = app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position;
            Vector3 newPos = new Vector3(app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position.x + i / 10f, app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position.y,
                                         app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position.z);
            app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position = newPos;


        }
    }
        
    public void MoveTrailAlongPath()
    {
        app.Controller.GameSceneController.LineRendererRowController.SetTrailPaintingOn();

        for (float i = 0; i < 10; i++)
        {
            Vector3 odlPos = app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position;
            Vector3 newPos = new Vector3(app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position.x + i / 10f, app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position.y,
                                         app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position.z);
            app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position = newPos;


        }
    }

	public void ClearTrail()
	{
		app.View.GameSceneView.LineRendererRowView.LineRendererView.TrailRenderer.time = 0;
		
		app.View.GameSceneView.LineRendererRowView.LineRendererView.TrailRenderer.time = 3600;
	}

	public void DisableLineRendererView()
	{
		app.View.GameSceneView.LineRendererRowView.LineRendererView.gameObject.SetActive(false);
	}
	
	public void EnableLineRendererView()
	{
		app.View.GameSceneView.LineRendererRowView.LineRendererView.gameObject.SetActive(true);
	}

    public void SaveTrail()
    {
        SetTrailRecordingOff();

        SetTrailPaintingOff();

        app.View.GameSceneView.LineRendererRowView.LineRendererView.transform.position =
            app.Model.GameSceneModel.LineRendererRowModel.trailPositions[0].first;

        ClearTrail();

        app.Controller.PlayfabController.GetContentUploadURL();
    }

    public void LoadTrail()
    {
        app.Controller.PlayfabController.GetContentDownloadURL();
    }
}