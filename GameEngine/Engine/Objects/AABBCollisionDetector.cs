using Engine.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Objects
{
    public sealed class AABBCollisionDetector<P,A>
        where P : BaseGameObject
        where A : BaseGameObject
    {
        private List<P> _passiveObjects;

        public AABBCollisionDetector(List<P> passiveObjects)
        {
            _passiveObjects = passiveObjects;
        }

        public void DetectCollisions(A activeObject, Action<P,A> collisionHandler)
        {
            foreach (P passiveObject in _passiveObjects)
            {
                if (DetectCollision(passiveObject, activeObject))
                {
                    collisionHandler(passiveObject, activeObject);
                }
            }
        }

        public void DetectCollisions(List<A> activeObjects, Action<P,A> collisionHandler)
        {
            foreach (A activeObject in activeObjects)
            {
                DetectCollisions(activeObject, collisionHandler);
            }
        }

        private bool DetectCollision(P passiveObject, A activeObject)
        {
            foreach (BoundingBox passiveBoundingBox in passiveObject.BoundingBoxes)
            {
                foreach (BoundingBox activeBoundingBox in activeObject.BoundingBoxes)
                {
                    if (passiveBoundingBox.CollidesWith(activeBoundingBox))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
