using System;
using System.Collections.Generic;
using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Tool Gun utilities.
    /// </summary>
    public static class ToolGunUtil
    {
        /// <summary>
        /// Returns the <see cref="Direction"/> that forms the smallest angle with the vect param.
        /// </summary>
        /// <param name="vect">The vector used to form angles with each direction.</param>
        public static Direction GetClosestDirection(Vector3 vect)
        {
            Dictionary<Direction, Vector3> dirs = new Dictionary<Direction, Vector3>();
            dirs[Direction.UP] = Vector3.up;
            dirs[Direction.DOWN] = Vector3.down;
            dirs[Direction.LEFT] = Vector3.left;
            dirs[Direction.RIGHT] = Vector3.right;
            dirs[Direction.FORWARD] = Vector3.forward;
            dirs[Direction.BACK] = Vector3.back;

            float smallestAngle = Mathf.Infinity;
            Direction smallestDirection = Direction.NONE;
            foreach (var pair in dirs)
            {
                var angle = Vector3.Angle(vect, pair.Value);
                if (angle < smallestAngle)
                {
                    smallestAngle = angle;
                    smallestDirection = pair.Key;
                }
            }

            return smallestDirection;
        }
    }
}