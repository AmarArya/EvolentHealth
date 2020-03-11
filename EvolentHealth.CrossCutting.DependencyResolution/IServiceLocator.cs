using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolentHealth.CrossCutting.DependencyResolution
{
	public interface IServiceLocator
	{
		T GetInstance<T>();
		object GetInstance(Type type);
		IEnumerable<object> GetAll(Type serviceType);
		IEnumerable<T> GetAll<T>();
	}
}
