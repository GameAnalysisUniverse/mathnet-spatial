using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;

namespace MathNet.Spatial.Functions
{
    public class FOVFunctions
    {

        /// <summary>
        /// Returns the offset of a the actor looking straight at reciever (line of sight) -> 5° means he looking 5° away from directly looking at him.
        /// </summary>
        /// <param name="actorV"></param>
        /// <param name="actorYaw"></param>
        /// <param name="recieverV"></param>
        /// <returns></returns>
        public static double GetLOSOffset2D(Point2D actorV, float actorYaw, Point2D recieverV)
        {
            //Keep actor as origin of the koordinate system
            double dx = recieverV.X - actorV.X;
            double dy = recieverV.Y - actorV.Y;
            var yawRad = Angle.FromDegrees(-actorYaw).Radians;
            var aimX = (float)(actorV.X + Math.Cos(yawRad)); // Aim vector from Yaw
            var aimY = (float)(actorV.Y + Math.Sin(yawRad));

            var aimdx = aimX - actorV.X;
            var aimdy = aimY - actorV.Y;
            Vector2D aimvec = new Vector2D(new double[] { aimdx, aimdy, 0 });
            Vector2D recvec = new Vector2D(new double[] { dx, dy, 0 });
            double theta = aimvec.DotProduct(recvec);
            double radian_theta = Math.Acos(theta);
            double degree_theta = 0;
            if (radian_theta < 0)
                degree_theta = 180 - Angle.FromRadians(yawRad).Degrees;
            else
                degree_theta = Angle.FromRadians(yawRad).Degrees;

            return degree_theta;
        }

        /// <summary>
        /// Checks if reciever is within the field of view (FOV-Horizontal OR Vertical) of the actor
        /// </summary>
        /// <param name="posP1"></param>
        /// <param name="angleV"></param>
        /// <param name="angleH"></param>
        /// <param name="posP2"></param>
        /// <returns></returns>
        public static bool IsInHVFOV(Point3D actorV, float actorYaw, Point3D recieverV, float FOVDegree)
        {
            double degree_theta = GetLOSOffset2D(actorV.SubstractZ(), actorYaw, recieverV.SubstractZ());
            if (degree_theta <= FOVDegree / 2) // No max sight distance to restrict FOV
                return true;
            return false;
        }

        /// <summary>
        /// Test if a position is in HFOV and VFOV
        /// </summary>
        /// <param name="actorV"></param>
        /// <param name="actorYaw"></param>
        /// <param name="recieverV"></param>
        /// <param name="HFOVDegree"></param>
        /// <param name="VFOVDegree"></param>
        /// <returns></returns>
        public static bool IsInFOV(Point3D actorV, float actorYaw, float actorPitch, Point3D recieverV, float HFOVDegree, float VFOVDegree)
        {
            double degree_theta_yaw = GetLOSOffset2D(actorV.SubstractZ(), actorYaw, recieverV.SubstractZ());
            double degree_theta_pitch = GetLOSOffset2D(actorV.SubstractZ(), actorPitch, recieverV.SubstractZ());
            if (degree_theta_yaw <= HFOVDegree / 2 && degree_theta_pitch <= VFOVDegree / 2) // No max sight distance to restrict FOV
                return true;
            return false;
        }
    }
}
