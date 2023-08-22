using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFootstepBehaviour : MonoBehaviour
{
    private enum Player_MovementType
    {
        Walk,
        Crouch,
        Run
    }
    public enum Footstep_Surface
    {
        Asphalt,
        Wood,
        Concrete
    }

    private Player_MovementType _movementType;
    private CharacterController _charController;
    private float _raycastDistance = 10f;
    private LayerMask _groundLayerMask;
    private float _footstepTimer = 0;
    private float _footstepInterval;

    public float walkingFootstepInterval = 0.35f, runningFootstepInterval = 0.7f, crouchingFootstepInterval = 1.0f;
    public Footstep_Surface footstepSurface;

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _groundLayerMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        //Debug.Log(_charController.velocity.magnitude);
        _footstepTimer += Time.deltaTime;
        

        if (_charController.velocity.magnitude > 0.1f && _footstepTimer > _footstepInterval)
        {
            SetFootstepType();
            //SetGroundSurfaceMaterial();
            AkSoundEngine.SetSwitch("Footstep_Surface", footstepSurface.ToString(), gameObject);
            AkSoundEngine.PostEvent("Play_Player_Footstep", gameObject);
            _footstepTimer = 0;
        }
    }

    private void SetFootstepType()
    {
        if (_charController.velocity.magnitude > 3f && _charController.velocity.magnitude < 5f)
        {
            _footstepInterval = walkingFootstepInterval;
            _movementType = Player_MovementType.Walk;
        }
        else if (_charController.velocity.magnitude > 5f)
        {
            _footstepInterval = runningFootstepInterval;
            _movementType = Player_MovementType.Run;
        }
        else if (_charController.velocity.magnitude < 3f)
        {
            _footstepInterval = crouchingFootstepInterval;
            _movementType = Player_MovementType.Crouch;
        }
        AkSoundEngine.SetSwitch("Player_MovementType", _movementType.ToString(), gameObject);
    }

    /*private void SetGroundSurfaceMaterial()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastDistance, _groundLayerMask))
        {
            Debug.Log("Raycast Hitpoint: " + hit.point);
            Material groundMaterial = DetectMaterial(hit);
            Debug.Log("Player stepped on material: " + groundMaterial.name);

            if (groundMaterial.name.Contains("Concrete"))
                footstepSurface = Footstep_Surface.Concrete;
            else if (groundMaterial.name.Contains("Wood"))
                footstepSurface = Footstep_Surface.Wood;
            else if (groundMaterial.name.Contains("Asphalt"))
                footstepSurface = Footstep_Surface.Asphalt;
            else
                return;

            AkSoundEngine.SetSwitch("Footstep_Surface", footstepSurface.ToString(), gameObject);
        }
    }

    public static int GetSubMeshIndex(Mesh mesh, int triangleIndex)
    {
        if (!mesh.isReadable) return 0;
    
        int[] triangles = mesh.triangles;
        int totalTriangles = triangles.Length / 3;
        int trianglesPerSubMesh = totalTriangles / mesh.subMeshCount;
    
        for (int subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; subMeshIndex++)
        {
            int startTriangle = subMeshIndex * trianglesPerSubMesh * 3;
            int endTriangle = (subMeshIndex + 1) * trianglesPerSubMesh * 3;

            if (triangleIndex >= startTriangle && triangleIndex < endTriangle)
            {
                Debug.Log("Triangle index: " + triangleIndex + " in submesh: " + subMeshIndex);
                return subMeshIndex;
            }
        }
    
        return 0;
    }

    private Material DetectMaterial(RaycastHit hit)
    {
        if (hit.transform == null) 
            return null;

        var materials = hit.transform.GetComponent<MeshRenderer>().materials;
        var index = hit.triangleIndex;
        var mesh = hit.transform.GetComponent<MeshFilter>().mesh;
        var subMeshIndex = GetSubMeshIndex(mesh, index);
        
        Debug.Log("Triangle index:" + hit.triangleIndex);
        Debug.Log("Submesh index: " + subMeshIndex);
        Debug.Log("Material count: " + materials.Length);

        return materials[subMeshIndex];
    }*/
}
