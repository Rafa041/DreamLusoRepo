using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.PropertyImages.Queries.Retrieve;

public class RetrievePropertyImageQuery
{
}
public class PropertyImageResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string ImagePath { get; set; }
}
