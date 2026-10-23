# Pathfinding Benchmark

Compares the runtime performance of Dijkstra's algorithm vs. A* across various grids with different sizes and wall percentages.

## Functionality

- Grids reduce memory usage by only storing coordinates of walls instead of the state of every single cell.
- A* is implemented using a flexible heuristic via delegates, allowing the same search logic to execute Dijkstra's algorithm.
- Randomly generated grids are guaranteed to be solvable by generating a random path first and not allowing walls to be placed on this path. 
- The program iterates through predefined arrays of grid sizes and wall percentages, averaging many runs per configuration.
- Results are automatically exported into a .csv file for analysis and graphing.

## Prerequisites

- .NET 10 SDK or higher

## Usage

1. Clone the repository:
   ```bash
   git clone https://github.com/Lorenz331/pathfinding-benchmark.git
   cd pathfinding-benchmark
   ```
   
2. Run the project in Release for better performance:
    ```bash
   dotnet run --project PathfindingBenchmark -c Release
   ```
   
3. Find your generated results CSV.