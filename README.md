# WaysToIterate

I tried every possible (I think) for / foreach loop and I have ran benchmark in order to find which is the best way to loop.

Here the Results:

|                       Method |    Size |            Mean |         Error |        StdDev |          Median | Allocated |
|----------------------------- |-------- |----------------:|--------------:|--------------:|----------------:|----------:|
|                          For |     100 |        38.16 ns |      0.781 ns |      1.428 ns |        37.45 ns |         - |
|                      Foreach |     100 |        66.86 ns |      0.514 ns |      0.481 ns |        66.71 ns |         - |
|                 Foreach_Linq |     100 |       158.97 ns |      3.189 ns |      7.328 ns |       156.31 ns |      64 B |
|             Parallel_ForEach |     100 |     4,863.07 ns |     59.407 ns |     55.569 ns |     4,868.46 ns |    3962 B |
|                Parallel_Linq |     100 |    39,884.62 ns |    281.465 ns |    263.283 ns |    39,846.89 ns |   11640 B |
|                 ForEach_Span |     100 |        25.96 ns |      0.538 ns |      0.900 ns |        25.84 ns |         - |
|                     For_Span |     100 |        26.20 ns |      0.545 ns |      0.997 ns |        25.96 ns |         - |
| Unsafe_For_Span_GetReference |     100 |        41.17 ns |      0.772 ns |      0.645 ns |        41.02 ns |     424 B |
|                Unsafe_Faster |     100 |        52.72 ns |      3.589 ns |     10.240 ns |        48.67 ns |     424 B |
|            Normal_For_AsSpan |     100 |        63.53 ns |      1.201 ns |      2.166 ns |        63.40 ns |     424 B |
|        Weird_Loop_With_Range |     100 |        25.82 ns |      0.366 ns |      0.342 ns |        25.69 ns |         - |
|      Weird_Loop_With_Range_2 |     100 |        43.36 ns |      0.477 ns |      0.446 ns |        43.20 ns |         - |
|                          For |  100000 |    31,998.30 ns |    452.380 ns |    423.157 ns |    31,908.74 ns |         - |
|                      Foreach |  100000 |    63,196.64 ns |    395.794 ns |    350.861 ns |    63,015.57 ns |         - |
|                 Foreach_Linq |  100000 |   148,235.18 ns |    484.781 ns |    429.745 ns |   148,222.92 ns |      64 B |
|             Parallel_ForEach |  100000 |   196,116.28 ns |    561.069 ns |    497.373 ns |   196,097.83 ns |   10499 B |
|                Parallel_Linq |  100000 |   124,560.64 ns |  1,821.681 ns |  1,704.002 ns |   124,363.45 ns |   13628 B |
|                 ForEach_Span |  100000 |    21,338.71 ns |    344.190 ns |    321.955 ns |    21,274.46 ns |         - |
|                     For_Span |  100000 |    21,113.43 ns |    311.050 ns |    290.956 ns |    20,936.94 ns |         - |
| Unsafe_For_Span_GetReference |  100000 |   177,013.58 ns |  3,478.476 ns |  4,761.378 ns |   176,794.80 ns |  400066 B |
|                Unsafe_Faster |  100000 |   181,328.16 ns |  3,576.187 ns |  5,013.314 ns |   181,962.16 ns |  400066 B |
|            Normal_For_AsSpan |  100000 |   197,751.13 ns |  3,793.242 ns |  3,548.201 ns |   198,244.36 ns |  400066 B |
|        Weird_Loop_With_Range |  100000 |    20,973.96 ns |    202.570 ns |    189.484 ns |    20,933.62 ns |         - |
|      Weird_Loop_With_Range_2 |  100000 |    36,524.46 ns |    722.234 ns |  1,012.471 ns |    36,116.58 ns |         - |
|                          For | 1000000 |   320,138.35 ns |  4,157.313 ns |  3,888.753 ns |   320,159.42 ns |         - |
|                      Foreach | 1000000 |   636,651.36 ns |  6,877.064 ns |  6,432.810 ns |   636,420.41 ns |       1 B |
|                 Foreach_Linq | 1000000 | 1,499,929.99 ns | 12,535.457 ns | 11,725.675 ns | 1,499,308.98 ns |      65 B |
|             Parallel_ForEach | 1000000 |   494,339.40 ns |  5,141.900 ns |  4,293.719 ns |   493,649.17 ns |   10700 B |
|                Parallel_Linq | 1000000 |   496,931.06 ns |  7,772.326 ns | 13,815.302 ns |   496,869.78 ns |   13649 B |
|                 ForEach_Span | 1000000 |   211,279.14 ns |    803.872 ns |    751.942 ns |   211,187.43 ns |         - |
|                     For_Span | 1000000 |   211,023.06 ns |  1,051.755 ns |    932.353 ns |   210,628.99 ns |         - |
| Unsafe_For_Span_GetReference | 1000000 |   795,657.17 ns |  5,673.001 ns |  5,306.529 ns |   797,182.32 ns | 4000128 B |
|                Unsafe_Faster | 1000000 |   815,950.45 ns |  7,003.946 ns |  6,551.495 ns |   816,531.15 ns | 4000128 B |
|            Normal_For_AsSpan | 1000000 |   958,865.41 ns |  7,955.564 ns |  7,441.640 ns |   961,092.87 ns | 4000128 B |
|        Weird_Loop_With_Range | 1000000 |   208,670.06 ns |  1,598.393 ns |  1,495.138 ns |   208,742.63 ns |         - |
|      Weird_Loop_With_Range_2 | 1000000 |   354,130.01 ns |  1,740.435 ns |  1,628.004 ns |   353,590.82 ns |         - |


I have ran all tests in .NET 6 and I use as parameter Integer Numbers with Arrays of value 100, 100.000 and 1.000.000 values.
