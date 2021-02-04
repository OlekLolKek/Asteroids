using System;
using System.Numerics;


namespace DefaultNamespace
{
    public class PointModel
    {
        public event Action<BigInteger> OnPointsChanged = delegate(BigInteger i) {  };
        
        private BigInteger _points;

        public void AddPoints(int pointsToAdd)
        {
            _points += pointsToAdd;
            OnPointsChanged.Invoke(_points);
        }
    }
}