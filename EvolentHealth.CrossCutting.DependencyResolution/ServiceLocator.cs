using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolentHealth.CrossCutting.DependencyResolution
{
	public class ServiceLocator
	{

		private static IServiceLocator _current;
		public static IServiceLocator Current
		{
			get
			{
				return _current;
			}
		}

		public static void SetServiceLocator(Func<IServiceLocator> create)
		{
			if (create == null) throw new ArgumentNullException("create");
			_current = create();
		}
	}
}
