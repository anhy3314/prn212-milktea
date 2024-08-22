using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.BLL.Utils
{
    public class ConvertFunction
    {
        #region Convert Object to object
        public static TDestination ConvertObjectToObject<TDestination, TSource>(TSource source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = new Mapper(config);
            return mapper.Map<TDestination>(source);
        }
        #endregion

        #region Convert list Object to list object
        public static List<TDestination> ConvertListToList<TDestination, TSource>(List<TSource> sourceList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = new Mapper(config);
            return mapper.Map<List<TDestination>>(sourceList);
        }
        #endregion
    }
}
