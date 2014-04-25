namespace Knapsack
   ///<summary>A Knapsack solver</summary>
   type ISolver =
      ///<summary>Solve the knapsack problem given the max weight and the set of elements</summary>
      abstract Solve : int64 -> seq<IItem> -> seq<IItem>
      ///<summary>Gets the type of problem that this solver solves</summary>
      abstract Type : ProblemType with get