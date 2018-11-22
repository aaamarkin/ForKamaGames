using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Obi;
using TouchScript.Hit;

public class RopeOnSnapActionComponent : OnSnapActionComponent
{
//	private ObiParticleHandle _obiParticleHandle;
//	private ObiCollider obiColliderToAttach;
//	private Vector3 obiOffsetToAttach;
//	public ObiRope obiRope;

	void Start()
	{
//		_obiParticleHandle = GetComponent<ObiParticleHandle>();
	}

	public override void OnSnap(SnappableComponent snappableComponent)
	{
//		obiColliderToAttach = snappableComponent.SnapPointComponent.ObiCollider;
		
//		print(snappableComponent.SnapPointComponent.ObiCollider.transform.position);
//		print(snappableComponent.SnapCenter);
		
//		obiOffsetToAttach = snappableComponent.SnapPointComponent.ObiCollider.transform.InverseTransformPoint(snappableComponent.SnapCenter);
		
//		print(obiOffsetToAttach);
		
//		print(snappableComponent.SnapPointComponent.ObiCollider.transform.position - snappableComponent.SnapCenter);
//		obiOffsetToAttach =  new Vector3(0f, 0.01f, 0f);
		
		
		StartCoroutine(AttachHook());
//		AttachHook();


	}

	public override void OnUnSnap(SnappableComponent snappableComponent)
	{
		StartCoroutine(DetachHook());
//		DetachHook();
	}

//	private void AttachHook()
//	{
//		var particleIndex = _obiParticleHandle.FirstParticleIndex;
//
//
//		_obiParticleHandle.RemoveParticle(particleIndex);
//
//		_obiParticleHandle.enabled = false;
//
//		ObiPinConstraintBatch pinBatch = obiRope.PinConstraints.GetBatches()[0] as ObiPinConstraintBatch;
//
//		obiRope.PinConstraints.RemoveFromSolver(null);
////		obiRope.RemoveFromSolver(null);
//
//		pinBatch.AddConstraint(obiRope.particleIndices[particleIndex], obiColliderToAttach, obiOffsetToAttach, 0);
//		
//
//		obiRope.PinConstraints.AddToSolver(null);
////		obiRope.AddToSolver(null);
//		obiRope.PinConstraints.PushDataToSolver();
//		obiRope.PushDataToSolver();
//		
//	
//	}

	private IEnumerator AttachHook(){

		print("Attaching rope");
	
//		var particleIndex = obiRope.particleIndices.Length - 2;
		
		
//		var particleIndex = 25;
		
//		print(obiRope.particleIndices.Length);
//		print(_obiParticleHandle.FirstParticleIndex);
//		var particleIndex = _obiParticleHandle.FirstParticleIndex;
//
//		_obiParticleHandle.RemoveParticle(particleIndex);
//
//		_obiParticleHandle.
//		_obiParticleHandle.enabled = false;

//		ObiPinConstraintBatch pinBatch = obiRope.PinConstraints.GetBatches()[0] as ObiPinConstraintBatch;
//
//		obiRope.PinConstraints.RemoveFromSolver(null);
//
//		pinBatch.AddConstraint(obiRope.particleIndices[particleIndex], obiColliderToAttach, obiOffsetToAttach, 0);
//		
//		
//		obiRope.AddToSolver(null);
//		obiRope.PinConstraints.AddToSolver(null);
//		obiRope.PinConstraints.PushDataToSolver();
//		obiRope.PushDataToSolver();
		
		yield return 0;


//		this.transform.parent = obiColliderToAttach.transform;
	}    
	
	private IEnumerator DetachHook()
	{
//		print("Dettaching rope");
		
//		this.transform.parent = obiRope.transform;
//
//		var particleIndex = 0;
//
//		Vector3 position = _obiParticleHandle.transform.position;
		
//		print(obiRope.positions[particleIndex]);
//		Vector3 position = new Vector3(0, 0, 0);

//		ObiPinConstraintBatch pinConstraints = obiRope.PinConstraints.GetBatches()[0] as ObiPinConstraintBatch;
//		
//		obiRope.PinConstraints.RemoveFromSolver(null);
//		
//		pinConstraints.RemoveConstraint(obiRope.particleIndices[particleIndex]);
//
//		_obiParticleHandle.AddParticle(particleIndex, position, obiRope.invMasses[particleIndex]);
//		
//		obiRope.AddToSolver(null);
//		obiRope.PinConstraints.AddToSolver(null);
//		obiRope.PinConstraints.PushDataToSolver();
//		obiRope.PushDataToSolver();

		yield return 0;
	}
}
