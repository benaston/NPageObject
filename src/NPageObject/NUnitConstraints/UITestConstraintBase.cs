// Copyright 2012, Ben Aston (ben@bj.ma).
// 
// This file is part of NPageObject.
// 
// NPageObject is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NPageObject is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NPageObject. If not, see <http://www.gnu.org/licenses/>.

namespace NPageObject.NUnitConstraints
{
	using NUnit.Framework.Constraints;

	public abstract class UITestConstraintBase<T> : Constraint
		where T : IPageObject<T>, new()
	{
		protected IUITestContext<T> UITestContext { get; set; }

		public abstract override bool Matches(object actual);

		public abstract override void WriteDescriptionTo(MessageWriter writer);
	}
}

// ReSharper restore InconsistentNaming