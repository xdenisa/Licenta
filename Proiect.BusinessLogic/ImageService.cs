using Proiect.BusinessLogic.Base;
using Proiect.DataAccess;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proiect.BusinessLogic
{
    public class ImageService : BaseService
    {
        public ImageService(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Image InsertImage(Image image)
        {
            return unitOfWork.Images.Insert(image);
        }

        public void DeleteImage(Image image)
        {
            unitOfWork.Images.Delete(image);
            unitOfWork.SaveChanges();
        }

        public Image GetImageById(Guid idImage)
        {
            return unitOfWork.Images.Get()
                .First(i => i.Id == idImage);
        }
    }
}
