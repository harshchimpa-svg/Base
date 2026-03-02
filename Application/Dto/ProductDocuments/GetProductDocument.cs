

using System.Reflection;
using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.GymProducts;
using Application.Features.GymProducts.Queries;
using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using Domain.Entities.GymProducts;
using Domain.Entities.ProductDocuments;
using static Emgu.CV.ML.KNearest;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Dto.ProductDocuments;

public class GetProductDocument : BaseDto, IMapFrom<ProductDocument>
{
    public string ImageUrl { get; set; }
    public int? GymProductId { get; set; }
}
