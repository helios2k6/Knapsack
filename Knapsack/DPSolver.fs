namespace Knapsack
   
   ///<summary>Uses dynamic programming to solve the 0/1 Knapsack problem</summary>
   type DPSolver =
      interface ISolver with
         override this.Type with get() = ZeroOne
         override this.Solve maxWeight items = this.SolveImpl maxWeight items
      end
      
      member private this.SolveImpl maxWeight items = Seq.empty