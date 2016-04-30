using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octogami.DutyHours.Application
{
	public interface ITest
	{
		void Throw(string message);
	}

	public class Test : ITest
	{
		public void Throw(string message)
		{
			throw new InvalidOperationException(message);
		}
	}
}
