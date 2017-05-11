using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using Domain.Abstract;
using Domain.Entities;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            Mock<IVideoRepository> mock = new Mock<IVideoRepository>();
            mock.Setup(m => m.Videos).Returns(new List<Video>
            {
                new Video { Name = "SimCity", Price = 1499 },
                new Video { Name = "TITANFALL", Price=2299 },
                new Video { Name = "Battlefield 4", Price=899.4M }
            });
            kernel.Bind<IVideoRepository>().ToConstant(mock.Object);
        }
    }
}