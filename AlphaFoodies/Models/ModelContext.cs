using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlphaFoodies.Models
{
    public class ModelContext
    {
        //Single ton
       private static AlphaFoodiesModel model;
        private ModelContext()
        {
            
        }

        public static AlphaFoodiesModel dbContext()
        {model = new AlphaFoodiesModel();
            return model;
        }
    }
}