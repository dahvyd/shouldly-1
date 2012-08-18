﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeTests
    {
        [Test]
        public void ShouldBe_WhenTrue_ShouldNotThrow()
        {
            true.ShouldBe(true);
        }

        [Test]
        public void ShouldNotBe_WhenTrue_ShouldNotThrow()
        {
            "this string".ShouldNotBe("some other string");
        }

        [Test]
        public void Should_WithNumbers_ShouldAllowTolerance()
        {
            Math.PI.ShouldNotBe(3.14);
            Math.PI.ShouldBe(3.14, 0.01);
            ((float)Math.PI).ShouldBe(3.14f, 0.01);
        }

        [Test]
        public void ShouldBe_GreaterThan()
        {
            7.ShouldBeGreaterThan(1);
            1.ShouldBeLessThan(7);
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 7.ShouldBeLessThan(0));
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 0.ShouldBeGreaterThan(7));
        }

        [Test]
        public void ShouldBe_GreaterThanOrEqualTo()
        {
            7.ShouldBeGreaterThanOrEqualTo(1);
            1.ShouldBeGreaterThanOrEqualTo(1);
            Shouldly.Should.Throw<ChuckedAWobbly>(() => 0.ShouldBeGreaterThanOrEqualTo(1));
        }

        [Test]
        public void ShouldBe_EnumerableValues_ShouldCompareItemsInEachEnumerable()
        {
            new[] { 1, 2 }.ShouldBe(new[] { 1, 2 });

            Should.Error(() =>
                         new[] { 2, 1 }.ShouldBe(new[] { 1, 2 }),
                         "new[] { 2, 1 } should be [1, 2] but was [2, 1] difference [*2*, *1*]"
                );
        }

        [Test]
        public void ShouldBe_EnumerableTypesOfDifferentRuntimeTypes_ShouldShowDifferences()
        {
            var a = new Widget { Name = "Joe", Enabled = true };
            var b = new Widget { Name = "Joeyjojoshabadoo Jr", Enabled = true };

            IEnumerable<Widget> aEnumerable = a.ToEnumerable();
            IEnumerable<Widget> bEnumerable = new[] { b };

            Should.Error(() =>
                aEnumerable.ShouldBe(bEnumerable),
                "aEnumerable should be [Name(Joeyjojoshabadoo Jr) Enabled(True)] but was [Name(Joe) Enabled(True)] difference [*Name(Joe) Enabled(True)*]"
            );
        }

        [Test]
        public void ShouldBe_EnumerableOfDoubles_ShouldAllowTolerance()
        {
            new[] { Math.PI, Math.PI }.ShouldBe(new[] { 3.14, 3.14 }, 0.01);
        }

        [Test]
        public void ShouldBeTypeOf_ShouldNotThrowForStrings()
        {
            "Sup yo".ShouldBeTypeOf(typeof(string));
        }

        [Test]
        public void ShouldBeTypeOfWithGenericParameter_ShouldNotThrowForStrings()
        {
            "Sup yo".ShouldBeTypeOf<string>();
        }

        [Test]
        public void ShouldNotBeTypeOf_ShouldNotThrowForNonMatchingType()
        {
            "Sup yo".ShouldNotBeTypeOf(typeof(int));
        }

        [Test]
        public void ShouldNotBeTypeOfWithGenericParameter_ShouldNotThrowForNonMatchingTypes()
        {
            "Sup yo".ShouldNotBeTypeOf<int>();
        }

        [Test]
        public void ShouldBe_ShouldNotThrowWhenCalledOnANullEnumerableReference()
        {
            IEnumerable<int> something = null;
            Should.Error(
                () => something.ShouldBe(new[] { 1, 2, 3 }),
                "() => something should be [1, 2, 3] but was null");
        }

        class MyBase { }
        class MyThing : MyBase { }

        [Test]
        public void ShouldBeTypeOf_ShouldNotThrowForInheritance()
        {
            new MyThing().ShouldBeTypeOf<MyBase>();
        }

        [Test]
        public void ShouldBe_Action()
        {
            Action a = () => 1.ShouldBe(2);

            Should.Error(a,
                "Action a = () => 1 should be 2 but was 1");
        }

        [Test]
        public void ShouldBe_Expression()
        {
            Expression<Action> lambda = () => 1.ShouldBe(2);

            Should.Error(lambda.Compile(),
            "The provided expression should be 2 but was 1");
        }

        [Test]
        public void ShouldBe_ShortAndInt()
        {
            short shorty = 1;
            shorty.ShouldBe(1);
        }

        [Test]
        public void ShouldBe_UShortAndInt()
        {
            ushort shorty = 1;
            shorty.ShouldBe(1);
        }

        [Test]
        public void ShouldBe_ByteTypeAndInt()
        {
            byte bighty = 1;
            bighty.ShouldBe(1);
        }

        [Test]
        public void ShouldBe_SByteTypeAndInt()
        {
            sbyte bighty = 1;
            bighty.ShouldBe(1);
            bighty.ShouldNotBe(-1);
        }

        [Test]
        public void ShouldNotBe_ShortAndInt()
        {
            short shorty = 1;
            shorty.ShouldNotBe(-1);
        }

        [Test]
        public void ShouldNotBe_UShortAndInt()
        {
            ushort shorty = 1;
            shorty.ShouldNotBe(-1);
        }

        [Test]
        public void ShouldNotBe_ByteTypeAndInt()
        {
            byte bighty = 1;
            bighty.ShouldNotBe(-1);
        }

        [Test]
        public void ShouldNotBe_SByteTypeAndInt()
        {
            sbyte bighty = 1;
            bighty.ShouldNotBe(-1);
        }

        [Test]
        public void ShouldNotBe_MyStructAndInt()
        {
            var myStruct = new MyStruct();
            myStruct.ShouldNotBe(1);
        }

		[Test]
		public void ShouldBe_NullableShortAndInt()
		{
			short? shorty = 1;
			shorty.ShouldBe(1);
		}

		[Test]
		public void ShouldBe_NullableUShortAndInt()
		{
			ushort? shorty = 1;
			shorty.ShouldBe(1);
		}

		[Test]
		public void ShouldBe_NullableByteTypeAndInt()
		{
			byte? bighty = 1;
			bighty.ShouldBe(1);
		}

		[Test]
		public void ShouldBe_NullableSByteTypeAndInt()
		{
			sbyte? bighty = 1;
			bighty.ShouldBe(1);
		}

		[Test]
		public void ShouldNotBe_NullableShortAndInt()
		{
			short? shorty = 1;
			shorty.ShouldNotBe(-1);
		}

		[Test]
		public void ShouldNotBe_NullableUShortAndInt()
		{
			ushort? shorty = 1;
			shorty.ShouldNotBe(-1);
		}

		[Test]
		public void ShouldNotBe_NullableByteTypeAndInt()
		{
			byte? bighty = 1;
			bighty.ShouldNotBe(-1);
		}

		[Test]
		public void ShouldNotBe_NullableSByteTypeAndInt()
		{
			sbyte? bighty = 1;
			bighty.ShouldNotBe(-1);
		}

		[Test]
		public void ShouldNotBe_NullableMyStructAndInt()
		{
			var myStruct = new MyStruct?();
			myStruct.ShouldNotBe(1);
		}

		[Test]
		public void ShouldBe_ObjectAndInt()
		{
			object one = 1;
			one.ShouldBe(1);
		}

		[Test]
		public void ShouldNotBe_ObjectAndInt()
		{
			object one = 1;
			one.ShouldNotBe(2);
		}
        
        #region MyStruct

        struct MyStruct : IConvertible
        {
            public TypeCode GetTypeCode()
            {
                throw new NotImplementedException();
            }

            public bool ToBoolean(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public byte ToByte(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public char ToChar(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public DateTime ToDateTime(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public decimal ToDecimal(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public double ToDouble(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public short ToInt16(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public int ToInt32(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public long ToInt64(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public sbyte ToSByte(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public float ToSingle(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public string ToString(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public object ToType(Type conversionType, IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public ushort ToUInt16(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public uint ToUInt32(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public ulong ToUInt64(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
