using System;
using MathNet.Spatial.Units;
using MathNet.Spatial.Functions;

namespace MathNet.Spatial.Euclidean
{
    [Serializable]
    public struct Circle2D
    {
        public readonly Point2D CenterPoint;
        public readonly double Radius;

        public Circle2D(Point2D centerPoint, double radius)
        {
            this.CenterPoint = centerPoint;
            this.Radius = radius;
        }

        /// <summary>
        /// Create a circle from the midpoint between two points, in a direction along a specified axis
        /// </summary>
        /// <param name="p1">First point on the circumference of the circle</param>
        /// <param name="p2">Second point on the circumference of the circle</param>
        /// <param name="axis">Direction of the plane in which the circle lies</param>
        public Circle2D(Point2D p1, Point2D p2)
        {
            this.CenterPoint = Point2D.MidPoint(p1, p2);
            this.Radius = p1.DistanceTo(CenterPoint);
        }

        public double Diameter => 2 * Radius;
        public double Circumference => 2 * Math.PI * Radius;
        public double Area => Math.PI * Math.Pow(Radius, 2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sphereCenterX">Center of circle in X</param>
        /// <param name="sphereCenterY">Center of circle in Y</param>
        /// <param name="sphereRadius">Radius of the circle</param>
        /// <param name="actorpos">Position of a player</param>
        /// <param name="actorYaw">Sight direction of this player</param>
        /// <returns></returns>
        public bool CircleIntersectsVector2D(Point2D actorpos, float actorYaw)
        {
            // Yaw has to be negated (csgo -> normal)
            var yawRad = Angle.FromDegrees(-actorYaw).Radians;
            var aimX = (float)(actorpos.X + Math.Cos(yawRad)); // Aim vector from Yaw 
            var aimY = (float)(actorpos.Y + Math.Sin(yawRad));

            // compute the euclidean distance between actor and aim
            var distanceActorAim = DistanceFunctions.GetEuclidDistance2D(actorpos, new Point2D(aimX, aimY));

            // compute the direction vector D from Actor to aimvector
            var dx = (actorpos.X - aimX) / distanceActorAim;
            var dy = (actorpos.Y - aimY) / distanceActorAim;

            // Now the line equation is x = dx*t + aimX, y = dy*t + aimY with 0 <= t <= 1.
            // compute the value t of the closest point to the circle center C(sphereCenterX, sphereCenterY)
            var t = dx * (CenterPoint.X - aimX) + dy * (CenterPoint.Y - aimY);

            // This is the projection of C on the line from actor to aim.
            // compute the coordinates of the point E on line and closest to C
            var ex = t * dx + aimX;
            var ey = t * dy + aimY;

            // compute the euclidean distance from E to C
            var distanceEC = DistanceFunctions.GetEuclidDistance2D(new Point2D((float)ex, (float)ey), new Point2D(CenterPoint.X, CenterPoint.Y));

            // test if the line intersects the circle
            if (distanceEC < Radius)
                return true;
            else if (distanceEC == Radius) // line is tangent to circle
                return true;
            else // line doesnt touch circle
                return false;
        }
    }
}
