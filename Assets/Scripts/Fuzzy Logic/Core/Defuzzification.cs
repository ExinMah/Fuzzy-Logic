using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Fuzzy_Logic
{
    [Serializable]
    public class Defuzzification : Fuzzification
    {
        // Editor only gui
        private IGUI _gui = null;
        public IGUI gui
        {
            set
            {
                _gui = value;
            }
            get
            {
                return _gui;
            }
        }

        private FuzzyLogicSystemList<Vector2> _bottomPoints = null;
        private FuzzyLogicSystemList<Vector2> bottomPoints
        {
            get
            {
                if (_bottomPoints == null)
                {
                    _bottomPoints = new FuzzyLogicSystemList<Vector2>();
                }
                return _bottomPoints;
            }
        }

        private FuzzyLogicSystemList<Vector2> _bottomPositions = null;
        private FuzzyLogicSystemList<Vector2> bottomPositions
        {
            get
            {
                if (_bottomPositions == null)
                {
                    _bottomPositions = new FuzzyLogicSystemList<Vector2>();
                }
                return _bottomPositions;
            }
        }

        private FuzzyLogicSystemList<Vector2> _shapePoints = null;
        private FuzzyLogicSystemList<Vector2> shapePoints
        {
            get
            {
                if (_shapePoints == null)
                {
                    _shapePoints = new FuzzyLogicSystemList<Vector2>();
                }
                return _shapePoints;
            }
        }

        private FuzzyLogicSystemList<Vector2> _shapePositions = null;
        private FuzzyLogicSystemList<Vector2> shapePositions
        {
            get
            {
                if (_shapePositions == null)
                {
                    _shapePositions = new FuzzyLogicSystemList<Vector2>();
                }
                return _shapePositions;
            }
        }

        private FuzzyLogicSystemList<float> _sampleValues = null;
        private FuzzyLogicSystemList<float> sampleValues
        {
            get
            {
                if (_sampleValues == null)
                {
                    _sampleValues = new FuzzyLogicSystemList<float>();
                }
                return _sampleValues;
            }
        }

        // precision of calculated barycenter
        // the more preciser the more costed cpu time 
        [SerializeField]
        private int _subdivision = 20;
        public int subdivision
        {
            set
            {
                _subdivision = value;
            }
            get
            {
                return _subdivision;
            }
        }

        public override float value
        {
            set
            {
                _value = value;
            }
        }

        public Defuzzification(string guid, FuzzyLogic fuzzyLogic) :
            base(guid, fuzzyLogic)
        {
            var leftShoulder = GetTrapezoid(0);
            leftShoulder.limitedValue = false;
            leftShoulder.peakPointLeftValue = 0;
            leftShoulder.peakPointRightValue = 0;
            leftShoulder.limitedValue = true;
            leftShoulder.footPointLeftValue = MinValue() * 0.5f;
            leftShoulder.footPointRightValue = Mathf.Abs(leftShoulder.footPointLeftValue);

            var rightShoulder = GetTrapezoid(1);
            leftShoulder.limitedValue = false;
            rightShoulder.peakPointLeftValue = maxValue;
            rightShoulder.peakPointRightValue = maxValue;
            leftShoulder.limitedValue = true;
            rightShoulder.footPointLeftValue = maxValue - (MaxValue() - maxValue) * 0.5f;
            rightShoulder.footPointRightValue = maxValue + (MaxValue() - maxValue) * 0.5f;
            
        }

        public override float MaxExtensionScale()
        {
            return 0.5f;
        }

        public override float MinExtensionScale()
        {
            return 0.5f;
        }

        public Vector2 OutputValue(out Vector2[] o_shapePoints, out Vector2[] o_bottomPoints)
        {
            shapePoints.Clear();
            bottomPoints.Clear();

            float minValue = MaxValue() + 1;
            float maxValue = MinValue() - 1;
            for (int trapezoidI = 0; trapezoidI < NumberTrapezoids(); trapezoidI++)
            {
                var trapezoid = GetTrapezoid(trapezoidI);
                minValue = Mathf.Min(minValue, trapezoid.footPointLeftValue);
                maxValue = Mathf.Max(maxValue, trapezoid.footPointRightValue);
            }

            // sample points
            sampleValues.Clear();
            float sampleMinValue = minValue - 1;
            float sampleMaxValue = maxValue + 1;
            float sampleStepValue = (sampleMaxValue - sampleMinValue) / subdivision;
            // fixed step sample points
            for (float sampleValue = sampleMinValue; sampleValue < sampleMaxValue; sampleValue += sampleStepValue)
            {
                sampleValues.Add(sampleValue);
            }
            // peak and foot sample points
            for (int trapezoidI = 0; trapezoidI < NumberTrapezoids(); trapezoidI++)
            {
                var trapezoid = GetTrapezoid(trapezoidI);
                {
                    trapezoid.AdjustPeakPointByHeight(trapezoid.peakPointLeftValue, trapezoid.footPointLeftValue, out float o_peakPointValueX, out float _);
                    sampleValues.Add(o_peakPointValueX);
                    sampleValues.Add(trapezoid.footPointLeftValue);
                }
                {
                    trapezoid.AdjustPeakPointByHeight(trapezoid.peakPointRightValue, trapezoid.footPointRightValue, out float o_peakPointValueX, out float _);
                    sampleValues.Add(o_peakPointValueX);
                    sampleValues.Add(trapezoid.footPointRightValue);
                }
            }
            sampleValues.Sort(SampleValuesComparer);

            shapePoints.Add(new Vector2(minValue, 0)); // start point
            {
                for (int sampleValueI = 0; sampleValueI < sampleValues.size; sampleValueI++)
                {
                    float oldValue = value; // cache value
                    {
                        value = sampleValues[sampleValueI]; // set sample value
                        TestIntersectionValuesOfBaseLineAndTrapozoids(out Vector2[] intersectionValues, out TrapezoidFuzzySet[] _);
                        if (intersectionValues != null && intersectionValues.Length > 0) // at least one intersection
                        {
                            // If there are several intersections, we fetch the one that y is maximal.
                            Vector2 highestIntersectionValue = new Vector2(0, -99999);
                            foreach (var intersectionValue in intersectionValues)
                            {
                                if (intersectionValue.y > highestIntersectionValue.y)
                                {
                                    highestIntersectionValue = intersectionValue;
                                }
                            }
                            shapePoints.Add(highestIntersectionValue);
                        }
                    }
                    value = oldValue; // restore value
                }
            }
            shapePoints.Add(new Vector2(maxValue, 0)); // end point

            for (int i = 0; i < shapePoints.size; i++)
            {
                Vector2 p = shapePoints[i];
                p.y = 0;
                bottomPoints.Add(p);
            }

            o_shapePoints = shapePoints.ToArray();
            o_bottomPoints = shapePoints.ToArray();

            return GetBarycenterPoint(shapePoints);
        }

        public Vector2 OutputPosition(Vector2 outputValue, Vector2 originalPos, Vector2 xAxisMaxPos, Vector2 yAxisMaxPos, out Vector2[] o_shapePositions, out Vector2[] o_bottomPositions)
        {
            shapePositions.Clear();
            bottomPositions.Clear();

            for (int i = 0; i < shapePoints.size; i++)
            {
                var p = shapePoints[i];
                shapePositions.Add(GetTrapezoid(0).ConvertValuesToPos(p.x, p.y, originalPos, xAxisMaxPos, yAxisMaxPos));
            }
            for (int i = 0; i < bottomPoints.size; i++)
            {
                var p = bottomPoints[i];
                bottomPositions.Add(GetTrapezoid(0).ConvertValuesToPos(p.x, p.y, originalPos, xAxisMaxPos, yAxisMaxPos));
            }

            o_shapePositions = shapePositions.ToArray();
            o_bottomPositions = bottomPositions.ToArray();

            return GetTrapezoid(0).ConvertValuesToPos(outputValue.x, outputValue.y, originalPos, xAxisMaxPos, yAxisMaxPos);
        }

        private Vector2 GetBarycenterPoint(FuzzyLogicSystemList<Vector2> mPoints)
        {
            float area = 0.0f;
            float Gx = 0.0f, Gy = 0.0f;
            for (int i = 1; i <= mPoints.size; i++)
            {
                float iLat = mPoints[i % mPoints.size].x;
                float iLng = mPoints[i % mPoints.size].y;
                float nextLat = mPoints[i - 1].x;
                float nextLng = mPoints[i - 1].y;
                float temp = (iLat * nextLng - iLng * nextLat) / 2.0f;
                area += temp;
                Gx += temp * (iLat + nextLat) / 3.0f;
                Gy += temp * (iLng + nextLng) / 3.0f;
            }
            Gx = Gx / area;
            Gy = Gy / area;
            return new Vector2(Gx, Gy);
        }

        private int SampleValuesComparer(float left, float right)
        {
            return left < right ? -1 : 1;
        }
    }
}