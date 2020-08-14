using UnityEngine;

namespace TC.Core
{
	public static class RayUtil
	{
		#region Ray

		public static Ray GetRayByScreenPos (Camera camera, Vector3 screenPos)
		{
			return camera.ScreenPointToRay (screenPos);
		}

		public static Ray GetRayByViewPort (Camera camera, Vector3 viewPort)
		{
			return camera.ViewportPointToRay (viewPort);
		}

		public static Ray GetRayByWorldPos (Camera camera, Vector3 worldPos)
		{
			var position = camera.transform.position;
			return new Ray (position, worldPos - position);
		}

		#endregion


		#region Raycast

		public static bool RaycastByScreenPos (Camera camera, Vector3 screenPos, out RaycastHit hit, float maxDistance = Mathf.Infinity,
			int layerMask = Physics.DefaultRaycastLayers)
		{
			var ray = GetRayByScreenPos (camera, screenPos);
			return Physics.Raycast (ray, out hit, maxDistance, layerMask);
		}

		public static Collider RaycastByScreenPos (Camera camera, Vector3 screenPos, float maxDistance = Mathf.Infinity,
			int layerMask = Physics.DefaultRaycastLayers)
		{
			RaycastHit hit;
			return RaycastByScreenPos (camera, screenPos, out hit, maxDistance, layerMask) ? hit.collider : null;
		}

		public static bool RaycastByViewPort (Camera camera, Vector3 viewPort, out RaycastHit hit, float maxDistance,
			int layerMask = Physics.DefaultRaycastLayers)
		{
			var ray = GetRayByViewPort (camera, viewPort);
			return Physics.Raycast (ray, out hit, maxDistance, layerMask);
		}

		public static Collider RaycastByViewPort (Camera camera, Vector3 viewPort, float maxDistance = Mathf.Infinity,
			int layerMask = Physics.DefaultRaycastLayers)
		{
			RaycastHit hit;
			return RaycastByViewPort (camera, viewPort, out hit, maxDistance, layerMask) ? hit.collider : null;
		}

		public static bool RaycastByWorldPos (Camera camera, Vector3 worldPos, out RaycastHit hit, float maxDistance = Mathf.Infinity,
			int layerMask = Physics.DefaultRaycastLayers)
		{
			var ray = GetRayByWorldPos (camera, worldPos);
			return Physics.Raycast (ray, out hit, maxDistance, layerMask);
		}

		public static Collider RaycastByWorldPos (Camera camera, Vector3 worldPos, float maxDistance = Mathf.Infinity,
			int layerMask = Physics.DefaultRaycastLayers)
		{
			RaycastHit hit;
			return RaycastByWorldPos (camera, worldPos, out hit, maxDistance, layerMask) ? hit.collider : null;
		}

		public static Collider Raycast (Ray ray, float maxDistance, int layerMask = Physics.DefaultRaycastLayers)
		{
			RaycastHit hit;
			return Physics.Raycast (ray, out hit, maxDistance, layerMask) ? hit.collider : null;
		}

		#endregion


		#region Ray to Position

		public static Vector3 GetWorldPosByX (Ray ray, float worldPosX)
		{
			var t = (worldPosX - ray.origin.x) / ray.direction.x;
			return ray.GetPoint (t);
		}

		public static Vector3 GetWorldPosByX (Camera camera, Vector3 screenPos, float worldPosX)
		{
			var ray = GetRayByScreenPos (camera, screenPos);
			return GetWorldPosByX (ray, worldPosX);
		}

		public static Vector3 GetWorldPosByY (Ray ray, float worldPosY)
		{
			var t = (worldPosY - ray.origin.y) / ray.direction.y;
			return ray.GetPoint (t);
		}

		public static Vector3 GetWorldPosByY (Camera camera, Vector3 screenPos, float worldPosY)
		{
			var ray = GetRayByScreenPos (camera, screenPos);
			return GetWorldPosByY (ray, worldPosY);
		}

		public static Vector3 GetWorldPosByZ (Ray ray, float worldPosZ)
		{
			var t = (worldPosZ - ray.origin.z) / ray.direction.z;
			return ray.GetPoint (t);
		}

		public static Vector3 GetWorldPosByZ (Camera camera, Vector3 screenPos, float worldPosZ)
		{
			var ray = GetRayByScreenPos (camera, screenPos);
			return GetWorldPosByZ (ray, worldPosZ);
		}

		#endregion
	}
}