namespace WebRunApplication.Models
{
    public static class StatisticalCalculator
    {
        public static List<MovingAverage> GetMovingAverages(List<double> values)
        {
            var movingAverages = new List<MovingAverage>();

            for (var i = 0; i < values.Count; ++i)
            {
                movingAverages.Add(new MovingAverage
                {
                    Id = (uint)(i + 1),
                    Value = values[i],
                    L3 = (i == 0 || i == values.Count - 1) ? null : (values[i - 1] + values[i] + values[i + 1]) / 3,
                    L7 = (i > 2 && i < values.Count - 3) ? (values[i - 3] + values[i - 2] + values[i - 1] + values[i]
                        + values[i + 1] + values[i + 2] + values[i + 3]) / 7 : null,
                    L5 = (i > 1 && i < values.Count - 2) ? (-3 * values[i - 2] + 12 * values[i - 1] + 17 * values[i] + 12 * values[i + 1]
                        - 3 * values[i + 2]) / 35 : null
                });
            }

            return movingAverages;
        }

        public static List<MovingAverage> GetMovingAveragesWithEdgeValues(List<double> values)
        {
            var movingAverages = new List<MovingAverage>();

            for (var i = 0; i < values.Count; ++i)
            {
                double? l3, l5, l7;
                var valuesCount = values.Count;

                switch (i)
                {
                    case 0:
                        l3 = (5 * values[0] + 2 * values[1] - values[2]) / 6;
                        l5 = (31 * values[0] + 9 * values[1] - 3 * values[2] - 5 * values[3] + 3 * values[4]) / 35;
                        l7 = (39 * values[0] + 8 * values[1] - 4 * values[2] - 4 * values[3] + 1 * values[4] + 4 * values[5] -
                              2 * values[6]) / 42;
                        break;
                    case 1:
                        l3 = (values[i - 1] + values[i] + values[i + 1]) / 3;
                        l5 = (9 * values[0] + 13 * values[1] + 12 * values[2] + 6 * values[3] + -5 * values[4]) / 35;
                        l7 = (8 * values[0] + 19 * values[1] + 16 * values[2] + 6 * values[3] - 4 * values[4] - 7 * values[5] +
                              4 * values[6]) / 42;
                        break;
                    case 2:
                        l3 = (values[i - 1] + values[i] + values[i + 1]) / 3;
                        l5 = (-3 * values[i - 2] + 12 * values[i - 1] + 17 * values[i] + 12 * values[i + 1] -
                              3 * values[i + 2]) / 35;
                        l7 = (-4 * values[0] + 16 * values[1] + 19 * values[2] + 12 * values[3] + 2 * values[4] -
                            4 * values[5] + values[6]) / 42;
                        break;
                    default:
                        {
                            if (i == valuesCount - 3)
                            {
                                l3 = (values[i - 1] + values[i] + values[i + 1]) / 3;
                                l5 = (-3 * values[i - 2] + 12 * values[i - 1] + 17 * values[i] + 12 * values[i + 1] -
                                      3 * values[i + 2]) / 35;

                                l7 = (values[valuesCount - 7] - 4 * values[valuesCount - 6] + 2 * values[valuesCount - 5] +
                                       12 * values[valuesCount - 4] + 19 * values[valuesCount - 3] + 16 * values[valuesCount - 2] -
                                       4 * values[valuesCount - 1]) / 42;
                            }
                            else if (i == valuesCount - 2)
                            {
                                l3 = (values[i - 1] + values[i] + values[i + 1]) / 3;
                                l5 = (-5 * values[valuesCount - 5] + 6 * values[valuesCount - 4] + 12 * values[valuesCount - 3] +
                                    13 * values[valuesCount - 2] - 9 * values[valuesCount - 1]) / 35;
                                l7 = (4 * values[valuesCount - 7] - 7 * values[valuesCount - 6] - 4 * values[valuesCount - 5] +
                                      6 * values[valuesCount - 4] + 16 * values[valuesCount - 3] + 19 * values[valuesCount - 2] +
                                      8 * values[valuesCount - 1]) / 42;

                            }
                            else if (i == valuesCount - 1)
                            {
                                l3 = (-values[valuesCount - 3] + 2 * values[valuesCount - 2] + 5 * values[valuesCount - 1]) / 6;

                                l5 = (3 * values[valuesCount - 5] - 5 * values[valuesCount - 4] - 3 * values[valuesCount - 3] +
                                      9 * values[valuesCount - 2] + 31 * values[valuesCount - 1]) / 35;

                                l7 = (2 * values[valuesCount - 7] + 4 * values[valuesCount - 6] + values[valuesCount - 5] -
                                      4 * values[valuesCount - 4] - 4 * values[valuesCount - 3] + 4 * values[valuesCount - 2] +
                                      39 * values[valuesCount - 1]) / 42;
                            }
                            else
                            {
                                l3 = (values[i - 1] + values[i] + values[i + 1]) / 3;
                                l5 = (-3 * values[i - 2] + 12 * values[i - 1] + 17 * values[i] + 12 * values[i + 1] -
                                      3 * values[i + 2]) / 35;

                                l7 = (values[i - 3] + values[i - 2] + values[i - 1] + values[i] + values[i + 1] + values[i + 2] +
                                      values[i + 3]) / 7;
                            }

                            break;
                        }
                }

                movingAverages.Add(new MovingAverage
                {
                    Id = (uint)(i + 1),
                    Value = values[i],
                    L3 = l3,
                    L7 = l7,
                    L5 = l5
                });
            }

            return movingAverages;
        }

        public static MovingAverage GetPredictiveValueByMovingAverages(List<MovingAverage> movingAverages)
        {

            var predictiveValueByMovingAverages = new MovingAverage
            {
                Id = (uint)(movingAverages.Count + 1),
                Value = null,
                L3 = movingAverages[^2].L3 +
                                      (movingAverages[^1].Value - movingAverages[^2].Value) / 3,
                L5 = movingAverages[^2].L5 +
                                     (movingAverages[^1].Value - movingAverages[^2].Value) / 5,
                L7 = movingAverages[^2].L7 +
                                      (movingAverages[^1].Value - movingAverages[^2].Value) / 7
            };

            return predictiveValueByMovingAverages;
        }
    }
}
