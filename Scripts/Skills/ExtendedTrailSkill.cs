using UnityEngine;

public class ExtendedTrailSkill : Skills{
    public float additionalDistance = 2f;

    public void ApplyExtendedTrail(GameObject hitObject) {
        if (isActive) {
            TrailRenderer trail = hitObject.GetComponent<TrailRenderer>();
            if (trail != null) {
                Vector3 originalPosition = hitObject.transform.position;
                Vector3 newPosition = originalPosition + hitObject.transform.forward * additionalDistance;
                
                trail.Clear();
                hitObject.transform.position = newPosition;
                trail.AddPosition(newPosition);
            }
        }
    }
}
