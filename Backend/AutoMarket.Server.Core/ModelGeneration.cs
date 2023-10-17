using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Server.Core
{
    public class ModelGeneration
    {
        public int ModelId { get; set; }
        public int GenerationId {  get; set; }
        
        public Model? Model { get; set; }
        public Generation? Generation {  get; set; }
    }
}
