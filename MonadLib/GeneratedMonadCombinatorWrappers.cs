﻿using System;
using System.Collections.Generic;

namespace MonadLib
{
	public static partial class Maybe
	{
		public static Maybe<TB> Bind<TA, TB>(this Maybe<TA> ma, Func<TA, Maybe<TB>> f) 
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (Maybe<TB>)monadAdapter.Bind(ma, f);
		}

		public static Maybe<TB> BindIgnoringLeft<TA, TB>(this Maybe<TA> ma, Maybe<TB> mb) 
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (Maybe<TB>)monadAdapter.BindIgnoringLeft(ma, mb);
		}

        public static Maybe<TB> Map<TA, TB>(Func<TA, TB> f, Maybe<TA> ma) 
        {
            return ma.Map(f);
        }
		
        public static Maybe<TB> Map<TA, TB>(this Maybe<TA> ma, Func<TA, TB> f) 
        {
            return ma.LiftM(f);
        }
		
        public static Maybe<TB> FlatMap<TA, TB>(Func<TA, Maybe<TB>> f, Maybe<TA> ma) 
        {
            return ma.Bind(f);
        }

        public static Maybe<TB> FlatMap<TA, TB>(this Maybe<TA> ma, Func<TA, Maybe<TB>> f) 
        {
            return ma.Bind(f);
        }
		
		public static Maybe<TB> LiftM<TA, TB>(Func<TA, TB> f, Maybe<TA> ma) 
		{
			return ma.LiftM(f);
		}

		public static Maybe<TB> LiftM<TA, TB>(this Maybe<TA> ma, Func<TA, TB> f) 
		{
			return (Maybe<TB>)MonadCombinators.LiftM(f, ma);
		}

		public static Maybe<TC> LiftM2<TA, TB, TC>(Func<TA, TB, TC> f, Maybe<TA> ma, Maybe<TB> mb) 
		{
			return ma.LiftM2(mb, f);
		}

		public static Maybe<TC> LiftM2<TA, TB, TC>(this Maybe<TA> ma, Maybe<TB> mb, Func<TA, TB, TC> f) 
		{
			return (Maybe<TC>)MonadCombinators.LiftM2(f, ma, mb);
		}

		public static Maybe<TD> LiftM3<TA, TB, TC, TD>(Func<TA, TB, TC, TD> f, Maybe<TA> ma, Maybe<TB> mb, Maybe<TC> mc) 
		{
			return ma.LiftM3(mb, mc, f);
		}

		public static Maybe<TD> LiftM3<TA, TB, TC, TD>(this Maybe<TA> ma, Maybe<TB> mb, Maybe<TC> mc, Func<TA, TB, TC, TD> f) 
		{
			return (Maybe<TD>)MonadCombinators.LiftM3(f, ma, mb, mc);
		}

		public static Maybe<TE> LiftM4<TA, TB, TC, TD, TE>(Func<TA, TB, TC, TD, TE> f, Maybe<TA> ma, Maybe<TB> mb, Maybe<TC> mc, Maybe<TD> md) 
		{
			return ma.LiftM4(mb, mc, md, f);
		}

		public static Maybe<TE> LiftM4<TA, TB, TC, TD, TE>(this Maybe<TA> ma, Maybe<TB> mb, Maybe<TC> mc, Maybe<TD> md, Func<TA, TB, TC, TD, TE> f) 
		{
			return (Maybe<TE>)MonadCombinators.LiftM4(f, ma, mb, mc, md);
		}

		public static Maybe<TF> LiftM5<TA, TB, TC, TD, TE, TF>(Func<TA, TB, TC, TD, TE, TF> f, Maybe<TA> ma, Maybe<TB> mb, Maybe<TC> mc, Maybe<TD> md, Maybe<TE> me) 
		{
			return ma.LiftM5(mb, mc, md, me, f);
		}

		public static Maybe<TF> LiftM5<TA, TB, TC, TD, TE, TF>(this Maybe<TA> ma, Maybe<TB> mb, Maybe<TC> mc, Maybe<TD> md, Maybe<TE> me, Func<TA, TB, TC, TD, TE, TF> f) 
		{
			return (Maybe<TF>)MonadCombinators.LiftM5(f, ma, mb, mc, md, me);
		}

		public static Maybe<IEnumerable<TA>> Sequence<TA>(IEnumerable<Maybe<TA>> ms) 
		{
			return (Maybe<IEnumerable<TA>>)MonadCombinators.SequenceInternal(ms, new MaybeMonadPlusAdapter<TA>());
		}

		// ReSharper disable InconsistentNaming
		public static Maybe<Unit> Sequence_<TA>(IEnumerable<Maybe<TA>> ms) 
		// ReSharper restore InconsistentNaming
		{
			return (Maybe<Unit>)MonadCombinators.SequenceInternal_(ms, new MaybeMonadPlusAdapter<TA>());
		}

		public static Maybe<IEnumerable<TB>> MapM<TA, TB>(Func<TA, Maybe<TB>> f, IEnumerable<TA> @as) 
		{
			return (Maybe<IEnumerable<TB>>)MonadCombinators.MapMInternal(f, @as, new MaybeMonadPlusAdapter<TA>());
		}

		// ReSharper disable InconsistentNaming
		public static Maybe<Unit> MapM_<TA, TB>(Func<TA, Maybe<TB>> f, IEnumerable<TA> @as) 
		// ReSharper restore InconsistentNaming
		{
			return (Maybe<Unit>)MonadCombinators.MapMInternal_(f, @as, new MaybeMonadPlusAdapter<TA>());
		}

		public static Maybe<IEnumerable<TB>> ForM<TA, TB>(IEnumerable<TA> @as, Func<TA, Maybe<TB>> f) 
		{
			return (Maybe<IEnumerable<TB>>)MonadCombinators.MapMInternal(f, @as, new MaybeMonadPlusAdapter<TA>());
		}

		// ReSharper disable InconsistentNaming
		public static Maybe<Unit> ForM_<TA, TB>(IEnumerable<TA> @as, Func<TA, Maybe<TB>> f) 
		// ReSharper restore InconsistentNaming
		{
			return (Maybe<Unit>)MonadCombinators.MapMInternal_(f, @as, new MaybeMonadPlusAdapter<TA>());
		}

		public static Maybe<IEnumerable<TA>> ReplicateM<TA>(int n, Maybe<TA> ma) 
		{
			return ma.ReplicateM(n);
		}

		public static Maybe<IEnumerable<TA>> ReplicateM<TA>(this Maybe<TA> ma, int n) 
		{
			return (Maybe<IEnumerable<TA>>)MonadCombinators.ReplicateM(n, ma);
		}

		// ReSharper disable InconsistentNaming
		public static Maybe<Unit> ReplicateM_<TA>(int n, Maybe<TA> ma) 
		// ReSharper restore InconsistentNaming
		{
			return ma.ReplicateM_(n);
		}

		// ReSharper disable InconsistentNaming
		public static Maybe<Unit> ReplicateM_<TA>(this Maybe<TA> ma, int n) 
		// ReSharper restore InconsistentNaming
		{
			return (Maybe<Unit>)MonadCombinators.ReplicateM_(n, ma);
		}

		public static Maybe<TA> Join<TA>(Maybe<Maybe<TA>> mma) 
		{
			// Ideally, we would like to use MonadCombinators.Join(mma) but there
			// is a casting issue that I have not figured out how to fix.
			var monadAdapter = mma.GetMonadAdapter();
			return (Maybe<TA>)monadAdapter.Bind(mma, MonadHelpers.Identity);
		}

		public static Maybe<TA> MFilter<TA>(Func<TA, bool> p, Maybe<TA> ma) 
		{
			return (Maybe<TA>)MonadPlusCombinators.MFilter(p, ma);
		}

		public static Maybe<TA> MFilter<TA>(this Maybe<TA> ma, Func<TA, bool> p) 
		{
			return (Maybe<TA>)MonadPlusCombinators.MFilter(p, ma);
		}

		public static Maybe<TA> FoldM<TA, TB>(Func<TA, TB, Maybe<TA>> f, TA a, IEnumerable<TB> bs) 
		{
			return (Maybe<TA>)MonadCombinators.FoldMInternal(f, a, bs, new MaybeMonadPlusAdapter<TA>());
		}

		// ReSharper disable InconsistentNaming
		public static Maybe<Unit> FoldM_<TA, TB>(Func<TA, TB, Maybe<TA>> f, TA a, IEnumerable<TB> bs) 
		// ReSharper restore InconsistentNaming
		{
			return (Maybe<Unit>)MonadCombinators.FoldMInternal_(f, a, bs, new MaybeMonadPlusAdapter<TA>());
		}

		public static Maybe<IEnumerable<TC>> ZipWithM<TA, TB, TC>(Func<TA, TB, Maybe<TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) 
		{
			return (Maybe<IEnumerable<TC>>)MonadCombinators.ZipWithMInternal(f, @as, bs, new MaybeMonadPlusAdapter<TA>());
		}

		// ReSharper disable InconsistentNaming
		public static Maybe<Unit> ZipWithM_<TA, TB, TC>(Func<TA, TB, Maybe<TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) 
		// ReSharper restore InconsistentNaming
		{
			return (Maybe<Unit>)MonadCombinators.ZipWithMInternal_(f, @as, bs, new MaybeMonadPlusAdapter<TA>());
		}

		public static Maybe<IEnumerable<TA>> FilterM<TA>(Func<TA, Maybe<bool>> p, IEnumerable<TA> @as) 
		{
			return (Maybe<IEnumerable<TA>>)MonadCombinators.FilterMInternal(p, @as, new MaybeMonadPlusAdapter<TA>());
		}

		public static Maybe<Unit> When(bool b, Maybe<Unit> m) 
		{
			return m.When(b);
		}

		public static Maybe<Unit> When(this Maybe<Unit> m, bool b) 
		{
			return (Maybe<Unit>)MonadCombinators.When(b, m);
		}

		public static Maybe<Unit> Unless(bool b, Maybe<Unit> m) 
		{
			return m.Unless(b);
		}

		public static Maybe<Unit> Unless(this Maybe<Unit> m, bool b) 
		{
			return (Maybe<Unit>)MonadCombinators.Unless(b, m);
		}

		public static Maybe<TB> Forever<TA, TB>(this Maybe<TA> m) 
		{
			return (Maybe<TB>)MonadCombinators.Forever<TA, TB>(m);
		}

		public static Maybe<Unit> Void<TA>(this Maybe<TA> m) 
		{
			return (Maybe<Unit>)MonadCombinators.Void(m);
		}

		public static Maybe<TB> Ap<TA, TB>(Maybe<Func<TA, TB>> mf, Maybe<TA> ma) 
		{
			return ma.Ap(mf);
		}

		public static Maybe<TB> Ap<TA, TB>(this Maybe<TA> ma, Maybe<Func<TA, TB>> mf) 
		{
			return (Maybe<TB>)MonadCombinators.Ap(mf, ma);
		}

		public static Func<TA, Maybe<TC>> Compose<TA, TB, TC>(Func<TA, Maybe<TB>> f, Func<TB, Maybe<TC>> g) 
		{
			return a => (Maybe<TC>)MonadCombinators.Compose(f, g)(a);
		}
	}

	public static partial class Either
	{
		public static Either<TLeft, TB> Bind<TLeft, TA, TB>(this Either<TLeft, TA> ma, Func<TA, Either<TLeft, TB>> f) 
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (Either<TLeft, TB>)monadAdapter.Bind(ma, f);
		}

		public static Either<TLeft, TB> BindIgnoringLeft<TLeft, TA, TB>(this Either<TLeft, TA> ma, Either<TLeft, TB> mb) 
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (Either<TLeft, TB>)monadAdapter.BindIgnoringLeft(ma, mb);
		}

        public static Either<TLeft, TB> Map<TLeft, TA, TB>(Func<TA, TB> f, Either<TLeft, TA> ma) 
        {
            return ma.Map(f);
        }
		
        public static Either<TLeft, TB> Map<TLeft, TA, TB>(this Either<TLeft, TA> ma, Func<TA, TB> f) 
        {
            return ma.LiftM(f);
        }
		
        public static Either<TLeft, TB> FlatMap<TLeft, TA, TB>(Func<TA, Either<TLeft, TB>> f, Either<TLeft, TA> ma) 
        {
            return ma.Bind(f);
        }

        public static Either<TLeft, TB> FlatMap<TLeft, TA, TB>(this Either<TLeft, TA> ma, Func<TA, Either<TLeft, TB>> f) 
        {
            return ma.Bind(f);
        }
		
		public static Either<TLeft, TB> LiftM<TLeft, TA, TB>(Func<TA, TB> f, Either<TLeft, TA> ma) 
		{
			return ma.LiftM(f);
		}

		public static Either<TLeft, TB> LiftM<TLeft, TA, TB>(this Either<TLeft, TA> ma, Func<TA, TB> f) 
		{
			return (Either<TLeft, TB>)MonadCombinators<TLeft>.LiftM(f, ma);
		}

		public static Either<TLeft, TC> LiftM2<TLeft, TA, TB, TC>(Func<TA, TB, TC> f, Either<TLeft, TA> ma, Either<TLeft, TB> mb) 
		{
			return ma.LiftM2(mb, f);
		}

		public static Either<TLeft, TC> LiftM2<TLeft, TA, TB, TC>(this Either<TLeft, TA> ma, Either<TLeft, TB> mb, Func<TA, TB, TC> f) 
		{
			return (Either<TLeft, TC>)MonadCombinators<TLeft>.LiftM2(f, ma, mb);
		}

		public static Either<TLeft, TD> LiftM3<TLeft, TA, TB, TC, TD>(Func<TA, TB, TC, TD> f, Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc) 
		{
			return ma.LiftM3(mb, mc, f);
		}

		public static Either<TLeft, TD> LiftM3<TLeft, TA, TB, TC, TD>(this Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Func<TA, TB, TC, TD> f) 
		{
			return (Either<TLeft, TD>)MonadCombinators<TLeft>.LiftM3(f, ma, mb, mc);
		}

		public static Either<TLeft, TE> LiftM4<TLeft, TA, TB, TC, TD, TE>(Func<TA, TB, TC, TD, TE> f, Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Either<TLeft, TD> md) 
		{
			return ma.LiftM4(mb, mc, md, f);
		}

		public static Either<TLeft, TE> LiftM4<TLeft, TA, TB, TC, TD, TE>(this Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Either<TLeft, TD> md, Func<TA, TB, TC, TD, TE> f) 
		{
			return (Either<TLeft, TE>)MonadCombinators<TLeft>.LiftM4(f, ma, mb, mc, md);
		}

		public static Either<TLeft, TF> LiftM5<TLeft, TA, TB, TC, TD, TE, TF>(Func<TA, TB, TC, TD, TE, TF> f, Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Either<TLeft, TD> md, Either<TLeft, TE> me) 
		{
			return ma.LiftM5(mb, mc, md, me, f);
		}

		public static Either<TLeft, TF> LiftM5<TLeft, TA, TB, TC, TD, TE, TF>(this Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Either<TLeft, TD> md, Either<TLeft, TE> me, Func<TA, TB, TC, TD, TE, TF> f) 
		{
			return (Either<TLeft, TF>)MonadCombinators<TLeft>.LiftM5(f, ma, mb, mc, md, me);
		}

		public static Either<TLeft, IEnumerable<TA>> Sequence<TLeft, TA>(IEnumerable<Either<TLeft, TA>> ms) 
		{
			return (Either<TLeft, IEnumerable<TA>>)MonadCombinators<TLeft>.SequenceInternal(ms, new EitherMonadAdapter<TLeft>());
		}

		// ReSharper disable InconsistentNaming
		public static Either<TLeft, Unit> Sequence_<TLeft, TA>(IEnumerable<Either<TLeft, TA>> ms) 
		// ReSharper restore InconsistentNaming
		{
			return (Either<TLeft, Unit>)MonadCombinators<TLeft>.SequenceInternal_(ms, new EitherMonadAdapter<TLeft>());
		}

		public static Either<TLeft, IEnumerable<TB>> MapM<TLeft, TA, TB>(Func<TA, Either<TLeft, TB>> f, IEnumerable<TA> @as) 
		{
			return (Either<TLeft, IEnumerable<TB>>)MonadCombinators<TLeft>.MapMInternal(f, @as, new EitherMonadAdapter<TLeft>());
		}

		// ReSharper disable InconsistentNaming
		public static Either<TLeft, Unit> MapM_<TLeft, TA, TB>(Func<TA, Either<TLeft, TB>> f, IEnumerable<TA> @as) 
		// ReSharper restore InconsistentNaming
		{
			return (Either<TLeft, Unit>)MonadCombinators<TLeft>.MapMInternal_(f, @as, new EitherMonadAdapter<TLeft>());
		}

		public static Either<TLeft, IEnumerable<TB>> ForM<TLeft, TA, TB>(IEnumerable<TA> @as, Func<TA, Either<TLeft, TB>> f) 
		{
			return (Either<TLeft, IEnumerable<TB>>)MonadCombinators<TLeft>.MapMInternal(f, @as, new EitherMonadAdapter<TLeft>());
		}

		// ReSharper disable InconsistentNaming
		public static Either<TLeft, Unit> ForM_<TLeft, TA, TB>(IEnumerable<TA> @as, Func<TA, Either<TLeft, TB>> f) 
		// ReSharper restore InconsistentNaming
		{
			return (Either<TLeft, Unit>)MonadCombinators<TLeft>.MapMInternal_(f, @as, new EitherMonadAdapter<TLeft>());
		}

		public static Either<TLeft, IEnumerable<TA>> ReplicateM<TLeft, TA>(int n, Either<TLeft, TA> ma) 
		{
			return ma.ReplicateM(n);
		}

		public static Either<TLeft, IEnumerable<TA>> ReplicateM<TLeft, TA>(this Either<TLeft, TA> ma, int n) 
		{
			return (Either<TLeft, IEnumerable<TA>>)MonadCombinators<TLeft>.ReplicateM(n, ma);
		}

		// ReSharper disable InconsistentNaming
		public static Either<TLeft, Unit> ReplicateM_<TLeft, TA>(int n, Either<TLeft, TA> ma) 
		// ReSharper restore InconsistentNaming
		{
			return ma.ReplicateM_(n);
		}

		// ReSharper disable InconsistentNaming
		public static Either<TLeft, Unit> ReplicateM_<TLeft, TA>(this Either<TLeft, TA> ma, int n) 
		// ReSharper restore InconsistentNaming
		{
			return (Either<TLeft, Unit>)MonadCombinators<TLeft>.ReplicateM_(n, ma);
		}

		public static Either<TLeft, TA> Join<TLeft, TA>(Either<TLeft, Either<TLeft, TA>> mma) 
		{
			// Ideally, we would like to use MonadCombinators<TLeft>.Join(mma) but there
			// is a casting issue that I have not figured out how to fix.
			var monadAdapter = mma.GetMonadAdapter();
			return (Either<TLeft, TA>)monadAdapter.Bind(mma, MonadHelpers.Identity);
		}


		public static Either<TLeft, TA> FoldM<TLeft, TA, TB>(Func<TA, TB, Either<TLeft, TA>> f, TA a, IEnumerable<TB> bs) 
		{
			return (Either<TLeft, TA>)MonadCombinators<TLeft>.FoldMInternal(f, a, bs, new EitherMonadAdapter<TLeft>());
		}

		// ReSharper disable InconsistentNaming
		public static Either<TLeft, Unit> FoldM_<TLeft, TA, TB>(Func<TA, TB, Either<TLeft, TA>> f, TA a, IEnumerable<TB> bs) 
		// ReSharper restore InconsistentNaming
		{
			return (Either<TLeft, Unit>)MonadCombinators<TLeft>.FoldMInternal_(f, a, bs, new EitherMonadAdapter<TLeft>());
		}

		public static Either<TLeft, IEnumerable<TC>> ZipWithM<TLeft, TA, TB, TC>(Func<TA, TB, Either<TLeft, TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) 
		{
			return (Either<TLeft, IEnumerable<TC>>)MonadCombinators<TLeft>.ZipWithMInternal(f, @as, bs, new EitherMonadAdapter<TLeft>());
		}

		// ReSharper disable InconsistentNaming
		public static Either<TLeft, Unit> ZipWithM_<TLeft, TA, TB, TC>(Func<TA, TB, Either<TLeft, TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) 
		// ReSharper restore InconsistentNaming
		{
			return (Either<TLeft, Unit>)MonadCombinators<TLeft>.ZipWithMInternal_(f, @as, bs, new EitherMonadAdapter<TLeft>());
		}

		public static Either<TLeft, IEnumerable<TA>> FilterM<TLeft, TA>(Func<TA, Either<TLeft, bool>> p, IEnumerable<TA> @as) 
		{
			return (Either<TLeft, IEnumerable<TA>>)MonadCombinators<TLeft>.FilterMInternal(p, @as, new EitherMonadAdapter<TLeft>());
		}

		public static Either<TLeft, Unit> When<TLeft>(bool b, Either<TLeft, Unit> m) 
		{
			return m.When(b);
		}

		public static Either<TLeft, Unit> When<TLeft>(this Either<TLeft, Unit> m, bool b) 
		{
			return (Either<TLeft, Unit>)MonadCombinators<TLeft>.When(b, m);
		}

		public static Either<TLeft, Unit> Unless<TLeft>(bool b, Either<TLeft, Unit> m) 
		{
			return m.Unless(b);
		}

		public static Either<TLeft, Unit> Unless<TLeft>(this Either<TLeft, Unit> m, bool b) 
		{
			return (Either<TLeft, Unit>)MonadCombinators<TLeft>.Unless(b, m);
		}

		public static Either<TLeft, TB> Forever<TLeft, TA, TB>(this Either<TLeft, TA> m) 
		{
			return (Either<TLeft, TB>)MonadCombinators<TLeft>.Forever<TA, TB>(m);
		}

		public static Either<TLeft, Unit> Void<TLeft, TA>(this Either<TLeft, TA> m) 
		{
			return (Either<TLeft, Unit>)MonadCombinators<TLeft>.Void(m);
		}

		public static Either<TLeft, TB> Ap<TLeft, TA, TB>(Either<TLeft, Func<TA, TB>> mf, Either<TLeft, TA> ma) 
		{
			return ma.Ap(mf);
		}

		public static Either<TLeft, TB> Ap<TLeft, TA, TB>(this Either<TLeft, TA> ma, Either<TLeft, Func<TA, TB>> mf) 
		{
			return (Either<TLeft, TB>)MonadCombinators<TLeft>.Ap(mf, ma);
		}

		public static Func<TA, Either<TLeft, TC>> Compose<TLeft, TA, TB, TC>(Func<TA, Either<TLeft, TB>> f, Func<TB, Either<TLeft, TC>> g) 
		{
			return a => (Either<TLeft, TC>)MonadCombinators<TLeft>.Compose(f, g)(a);
		}
	}

	public static partial class State
	{
		public static State<TS, TB> Bind<TS, TA, TB>(this State<TS, TA> ma, Func<TA, State<TS, TB>> f) 
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (State<TS, TB>)monadAdapter.Bind(ma, f);
		}

		public static State<TS, TB> BindIgnoringLeft<TS, TA, TB>(this State<TS, TA> ma, State<TS, TB> mb) 
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (State<TS, TB>)monadAdapter.BindIgnoringLeft(ma, mb);
		}

        public static State<TS, TB> Map<TS, TA, TB>(Func<TA, TB> f, State<TS, TA> ma) 
        {
            return ma.Map(f);
        }
		
        public static State<TS, TB> Map<TS, TA, TB>(this State<TS, TA> ma, Func<TA, TB> f) 
        {
            return ma.LiftM(f);
        }
		
        public static State<TS, TB> FlatMap<TS, TA, TB>(Func<TA, State<TS, TB>> f, State<TS, TA> ma) 
        {
            return ma.Bind(f);
        }

        public static State<TS, TB> FlatMap<TS, TA, TB>(this State<TS, TA> ma, Func<TA, State<TS, TB>> f) 
        {
            return ma.Bind(f);
        }
		
		public static State<TS, TB> LiftM<TS, TA, TB>(Func<TA, TB> f, State<TS, TA> ma) 
		{
			return ma.LiftM(f);
		}

		public static State<TS, TB> LiftM<TS, TA, TB>(this State<TS, TA> ma, Func<TA, TB> f) 
		{
			return (State<TS, TB>)MonadCombinators<TS>.LiftM(f, ma);
		}

		public static State<TS, TC> LiftM2<TS, TA, TB, TC>(Func<TA, TB, TC> f, State<TS, TA> ma, State<TS, TB> mb) 
		{
			return ma.LiftM2(mb, f);
		}

		public static State<TS, TC> LiftM2<TS, TA, TB, TC>(this State<TS, TA> ma, State<TS, TB> mb, Func<TA, TB, TC> f) 
		{
			return (State<TS, TC>)MonadCombinators<TS>.LiftM2(f, ma, mb);
		}

		public static State<TS, TD> LiftM3<TS, TA, TB, TC, TD>(Func<TA, TB, TC, TD> f, State<TS, TA> ma, State<TS, TB> mb, State<TS, TC> mc) 
		{
			return ma.LiftM3(mb, mc, f);
		}

		public static State<TS, TD> LiftM3<TS, TA, TB, TC, TD>(this State<TS, TA> ma, State<TS, TB> mb, State<TS, TC> mc, Func<TA, TB, TC, TD> f) 
		{
			return (State<TS, TD>)MonadCombinators<TS>.LiftM3(f, ma, mb, mc);
		}

		public static State<TS, TE> LiftM4<TS, TA, TB, TC, TD, TE>(Func<TA, TB, TC, TD, TE> f, State<TS, TA> ma, State<TS, TB> mb, State<TS, TC> mc, State<TS, TD> md) 
		{
			return ma.LiftM4(mb, mc, md, f);
		}

		public static State<TS, TE> LiftM4<TS, TA, TB, TC, TD, TE>(this State<TS, TA> ma, State<TS, TB> mb, State<TS, TC> mc, State<TS, TD> md, Func<TA, TB, TC, TD, TE> f) 
		{
			return (State<TS, TE>)MonadCombinators<TS>.LiftM4(f, ma, mb, mc, md);
		}

		public static State<TS, TF> LiftM5<TS, TA, TB, TC, TD, TE, TF>(Func<TA, TB, TC, TD, TE, TF> f, State<TS, TA> ma, State<TS, TB> mb, State<TS, TC> mc, State<TS, TD> md, State<TS, TE> me) 
		{
			return ma.LiftM5(mb, mc, md, me, f);
		}

		public static State<TS, TF> LiftM5<TS, TA, TB, TC, TD, TE, TF>(this State<TS, TA> ma, State<TS, TB> mb, State<TS, TC> mc, State<TS, TD> md, State<TS, TE> me, Func<TA, TB, TC, TD, TE, TF> f) 
		{
			return (State<TS, TF>)MonadCombinators<TS>.LiftM5(f, ma, mb, mc, md, me);
		}

		public static State<TS, IEnumerable<TA>> Sequence<TS, TA>(IEnumerable<State<TS, TA>> ms) 
		{
			return (State<TS, IEnumerable<TA>>)MonadCombinators<TS>.SequenceInternal(ms, new StateMonadAdapter<TS>());
		}

		// ReSharper disable InconsistentNaming
		public static State<TS, Unit> Sequence_<TS, TA>(IEnumerable<State<TS, TA>> ms) 
		// ReSharper restore InconsistentNaming
		{
			return (State<TS, Unit>)MonadCombinators<TS>.SequenceInternal_(ms, new StateMonadAdapter<TS>());
		}

		public static State<TS, IEnumerable<TB>> MapM<TS, TA, TB>(Func<TA, State<TS, TB>> f, IEnumerable<TA> @as) 
		{
			return (State<TS, IEnumerable<TB>>)MonadCombinators<TS>.MapMInternal(f, @as, new StateMonadAdapter<TS>());
		}

		// ReSharper disable InconsistentNaming
		public static State<TS, Unit> MapM_<TS, TA, TB>(Func<TA, State<TS, TB>> f, IEnumerable<TA> @as) 
		// ReSharper restore InconsistentNaming
		{
			return (State<TS, Unit>)MonadCombinators<TS>.MapMInternal_(f, @as, new StateMonadAdapter<TS>());
		}

		public static State<TS, IEnumerable<TB>> ForM<TS, TA, TB>(IEnumerable<TA> @as, Func<TA, State<TS, TB>> f) 
		{
			return (State<TS, IEnumerable<TB>>)MonadCombinators<TS>.MapMInternal(f, @as, new StateMonadAdapter<TS>());
		}

		// ReSharper disable InconsistentNaming
		public static State<TS, Unit> ForM_<TS, TA, TB>(IEnumerable<TA> @as, Func<TA, State<TS, TB>> f) 
		// ReSharper restore InconsistentNaming
		{
			return (State<TS, Unit>)MonadCombinators<TS>.MapMInternal_(f, @as, new StateMonadAdapter<TS>());
		}

		public static State<TS, IEnumerable<TA>> ReplicateM<TS, TA>(int n, State<TS, TA> ma) 
		{
			return ma.ReplicateM(n);
		}

		public static State<TS, IEnumerable<TA>> ReplicateM<TS, TA>(this State<TS, TA> ma, int n) 
		{
			return (State<TS, IEnumerable<TA>>)MonadCombinators<TS>.ReplicateM(n, ma);
		}

		// ReSharper disable InconsistentNaming
		public static State<TS, Unit> ReplicateM_<TS, TA>(int n, State<TS, TA> ma) 
		// ReSharper restore InconsistentNaming
		{
			return ma.ReplicateM_(n);
		}

		// ReSharper disable InconsistentNaming
		public static State<TS, Unit> ReplicateM_<TS, TA>(this State<TS, TA> ma, int n) 
		// ReSharper restore InconsistentNaming
		{
			return (State<TS, Unit>)MonadCombinators<TS>.ReplicateM_(n, ma);
		}

		public static State<TS, TA> Join<TS, TA>(State<TS, State<TS, TA>> mma) 
		{
			// Ideally, we would like to use MonadCombinators<TS>.Join(mma) but there
			// is a casting issue that I have not figured out how to fix.
			var monadAdapter = mma.GetMonadAdapter();
			return (State<TS, TA>)monadAdapter.Bind(mma, MonadHelpers.Identity);
		}


		public static State<TS, TA> FoldM<TS, TA, TB>(Func<TA, TB, State<TS, TA>> f, TA a, IEnumerable<TB> bs) 
		{
			return (State<TS, TA>)MonadCombinators<TS>.FoldMInternal(f, a, bs, new StateMonadAdapter<TS>());
		}

		// ReSharper disable InconsistentNaming
		public static State<TS, Unit> FoldM_<TS, TA, TB>(Func<TA, TB, State<TS, TA>> f, TA a, IEnumerable<TB> bs) 
		// ReSharper restore InconsistentNaming
		{
			return (State<TS, Unit>)MonadCombinators<TS>.FoldMInternal_(f, a, bs, new StateMonadAdapter<TS>());
		}

		public static State<TS, IEnumerable<TC>> ZipWithM<TS, TA, TB, TC>(Func<TA, TB, State<TS, TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) 
		{
			return (State<TS, IEnumerable<TC>>)MonadCombinators<TS>.ZipWithMInternal(f, @as, bs, new StateMonadAdapter<TS>());
		}

		// ReSharper disable InconsistentNaming
		public static State<TS, Unit> ZipWithM_<TS, TA, TB, TC>(Func<TA, TB, State<TS, TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) 
		// ReSharper restore InconsistentNaming
		{
			return (State<TS, Unit>)MonadCombinators<TS>.ZipWithMInternal_(f, @as, bs, new StateMonadAdapter<TS>());
		}

		public static State<TS, IEnumerable<TA>> FilterM<TS, TA>(Func<TA, State<TS, bool>> p, IEnumerable<TA> @as) 
		{
			return (State<TS, IEnumerable<TA>>)MonadCombinators<TS>.FilterMInternal(p, @as, new StateMonadAdapter<TS>());
		}

		public static State<TS, Unit> When<TS>(bool b, State<TS, Unit> m) 
		{
			return m.When(b);
		}

		public static State<TS, Unit> When<TS>(this State<TS, Unit> m, bool b) 
		{
			return (State<TS, Unit>)MonadCombinators<TS>.When(b, m);
		}

		public static State<TS, Unit> Unless<TS>(bool b, State<TS, Unit> m) 
		{
			return m.Unless(b);
		}

		public static State<TS, Unit> Unless<TS>(this State<TS, Unit> m, bool b) 
		{
			return (State<TS, Unit>)MonadCombinators<TS>.Unless(b, m);
		}

		public static State<TS, TB> Forever<TS, TA, TB>(this State<TS, TA> m) 
		{
			return (State<TS, TB>)MonadCombinators<TS>.Forever<TA, TB>(m);
		}

		public static State<TS, Unit> Void<TS, TA>(this State<TS, TA> m) 
		{
			return (State<TS, Unit>)MonadCombinators<TS>.Void(m);
		}

		public static State<TS, TB> Ap<TS, TA, TB>(State<TS, Func<TA, TB>> mf, State<TS, TA> ma) 
		{
			return ma.Ap(mf);
		}

		public static State<TS, TB> Ap<TS, TA, TB>(this State<TS, TA> ma, State<TS, Func<TA, TB>> mf) 
		{
			return (State<TS, TB>)MonadCombinators<TS>.Ap(mf, ma);
		}

		public static Func<TA, State<TS, TC>> Compose<TS, TA, TB, TC>(Func<TA, State<TS, TB>> f, Func<TB, State<TS, TC>> g) 
		{
			return a => (State<TS, TC>)MonadCombinators<TS>.Compose(f, g)(a);
		}
	}

	public static partial class Reader
	{
		public static Reader<TR, TB> Bind<TR, TA, TB>(this Reader<TR, TA> ma, Func<TA, Reader<TR, TB>> f) 
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (Reader<TR, TB>)monadAdapter.Bind(ma, f);
		}

		public static Reader<TR, TB> BindIgnoringLeft<TR, TA, TB>(this Reader<TR, TA> ma, Reader<TR, TB> mb) 
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (Reader<TR, TB>)monadAdapter.BindIgnoringLeft(ma, mb);
		}

        public static Reader<TR, TB> Map<TR, TA, TB>(Func<TA, TB> f, Reader<TR, TA> ma) 
        {
            return ma.Map(f);
        }
		
        public static Reader<TR, TB> Map<TR, TA, TB>(this Reader<TR, TA> ma, Func<TA, TB> f) 
        {
            return ma.LiftM(f);
        }
		
        public static Reader<TR, TB> FlatMap<TR, TA, TB>(Func<TA, Reader<TR, TB>> f, Reader<TR, TA> ma) 
        {
            return ma.Bind(f);
        }

        public static Reader<TR, TB> FlatMap<TR, TA, TB>(this Reader<TR, TA> ma, Func<TA, Reader<TR, TB>> f) 
        {
            return ma.Bind(f);
        }
		
		public static Reader<TR, TB> LiftM<TR, TA, TB>(Func<TA, TB> f, Reader<TR, TA> ma) 
		{
			return ma.LiftM(f);
		}

		public static Reader<TR, TB> LiftM<TR, TA, TB>(this Reader<TR, TA> ma, Func<TA, TB> f) 
		{
			return (Reader<TR, TB>)MonadCombinators<TR>.LiftM(f, ma);
		}

		public static Reader<TR, TC> LiftM2<TR, TA, TB, TC>(Func<TA, TB, TC> f, Reader<TR, TA> ma, Reader<TR, TB> mb) 
		{
			return ma.LiftM2(mb, f);
		}

		public static Reader<TR, TC> LiftM2<TR, TA, TB, TC>(this Reader<TR, TA> ma, Reader<TR, TB> mb, Func<TA, TB, TC> f) 
		{
			return (Reader<TR, TC>)MonadCombinators<TR>.LiftM2(f, ma, mb);
		}

		public static Reader<TR, TD> LiftM3<TR, TA, TB, TC, TD>(Func<TA, TB, TC, TD> f, Reader<TR, TA> ma, Reader<TR, TB> mb, Reader<TR, TC> mc) 
		{
			return ma.LiftM3(mb, mc, f);
		}

		public static Reader<TR, TD> LiftM3<TR, TA, TB, TC, TD>(this Reader<TR, TA> ma, Reader<TR, TB> mb, Reader<TR, TC> mc, Func<TA, TB, TC, TD> f) 
		{
			return (Reader<TR, TD>)MonadCombinators<TR>.LiftM3(f, ma, mb, mc);
		}

		public static Reader<TR, TE> LiftM4<TR, TA, TB, TC, TD, TE>(Func<TA, TB, TC, TD, TE> f, Reader<TR, TA> ma, Reader<TR, TB> mb, Reader<TR, TC> mc, Reader<TR, TD> md) 
		{
			return ma.LiftM4(mb, mc, md, f);
		}

		public static Reader<TR, TE> LiftM4<TR, TA, TB, TC, TD, TE>(this Reader<TR, TA> ma, Reader<TR, TB> mb, Reader<TR, TC> mc, Reader<TR, TD> md, Func<TA, TB, TC, TD, TE> f) 
		{
			return (Reader<TR, TE>)MonadCombinators<TR>.LiftM4(f, ma, mb, mc, md);
		}

		public static Reader<TR, TF> LiftM5<TR, TA, TB, TC, TD, TE, TF>(Func<TA, TB, TC, TD, TE, TF> f, Reader<TR, TA> ma, Reader<TR, TB> mb, Reader<TR, TC> mc, Reader<TR, TD> md, Reader<TR, TE> me) 
		{
			return ma.LiftM5(mb, mc, md, me, f);
		}

		public static Reader<TR, TF> LiftM5<TR, TA, TB, TC, TD, TE, TF>(this Reader<TR, TA> ma, Reader<TR, TB> mb, Reader<TR, TC> mc, Reader<TR, TD> md, Reader<TR, TE> me, Func<TA, TB, TC, TD, TE, TF> f) 
		{
			return (Reader<TR, TF>)MonadCombinators<TR>.LiftM5(f, ma, mb, mc, md, me);
		}

		public static Reader<TR, IEnumerable<TA>> Sequence<TR, TA>(IEnumerable<Reader<TR, TA>> ms) 
		{
			return (Reader<TR, IEnumerable<TA>>)MonadCombinators<TR>.SequenceInternal(ms, new ReaderMonadAdapter<TR>());
		}

		// ReSharper disable InconsistentNaming
		public static Reader<TR, Unit> Sequence_<TR, TA>(IEnumerable<Reader<TR, TA>> ms) 
		// ReSharper restore InconsistentNaming
		{
			return (Reader<TR, Unit>)MonadCombinators<TR>.SequenceInternal_(ms, new ReaderMonadAdapter<TR>());
		}

		public static Reader<TR, IEnumerable<TB>> MapM<TR, TA, TB>(Func<TA, Reader<TR, TB>> f, IEnumerable<TA> @as) 
		{
			return (Reader<TR, IEnumerable<TB>>)MonadCombinators<TR>.MapMInternal(f, @as, new ReaderMonadAdapter<TR>());
		}

		// ReSharper disable InconsistentNaming
		public static Reader<TR, Unit> MapM_<TR, TA, TB>(Func<TA, Reader<TR, TB>> f, IEnumerable<TA> @as) 
		// ReSharper restore InconsistentNaming
		{
			return (Reader<TR, Unit>)MonadCombinators<TR>.MapMInternal_(f, @as, new ReaderMonadAdapter<TR>());
		}

		public static Reader<TR, IEnumerable<TB>> ForM<TR, TA, TB>(IEnumerable<TA> @as, Func<TA, Reader<TR, TB>> f) 
		{
			return (Reader<TR, IEnumerable<TB>>)MonadCombinators<TR>.MapMInternal(f, @as, new ReaderMonadAdapter<TR>());
		}

		// ReSharper disable InconsistentNaming
		public static Reader<TR, Unit> ForM_<TR, TA, TB>(IEnumerable<TA> @as, Func<TA, Reader<TR, TB>> f) 
		// ReSharper restore InconsistentNaming
		{
			return (Reader<TR, Unit>)MonadCombinators<TR>.MapMInternal_(f, @as, new ReaderMonadAdapter<TR>());
		}

		public static Reader<TR, IEnumerable<TA>> ReplicateM<TR, TA>(int n, Reader<TR, TA> ma) 
		{
			return ma.ReplicateM(n);
		}

		public static Reader<TR, IEnumerable<TA>> ReplicateM<TR, TA>(this Reader<TR, TA> ma, int n) 
		{
			return (Reader<TR, IEnumerable<TA>>)MonadCombinators<TR>.ReplicateM(n, ma);
		}

		// ReSharper disable InconsistentNaming
		public static Reader<TR, Unit> ReplicateM_<TR, TA>(int n, Reader<TR, TA> ma) 
		// ReSharper restore InconsistentNaming
		{
			return ma.ReplicateM_(n);
		}

		// ReSharper disable InconsistentNaming
		public static Reader<TR, Unit> ReplicateM_<TR, TA>(this Reader<TR, TA> ma, int n) 
		// ReSharper restore InconsistentNaming
		{
			return (Reader<TR, Unit>)MonadCombinators<TR>.ReplicateM_(n, ma);
		}

		public static Reader<TR, TA> Join<TR, TA>(Reader<TR, Reader<TR, TA>> mma) 
		{
			// Ideally, we would like to use MonadCombinators<TR>.Join(mma) but there
			// is a casting issue that I have not figured out how to fix.
			var monadAdapter = mma.GetMonadAdapter();
			return (Reader<TR, TA>)monadAdapter.Bind(mma, MonadHelpers.Identity);
		}


		public static Reader<TR, TA> FoldM<TR, TA, TB>(Func<TA, TB, Reader<TR, TA>> f, TA a, IEnumerable<TB> bs) 
		{
			return (Reader<TR, TA>)MonadCombinators<TR>.FoldMInternal(f, a, bs, new ReaderMonadAdapter<TR>());
		}

		// ReSharper disable InconsistentNaming
		public static Reader<TR, Unit> FoldM_<TR, TA, TB>(Func<TA, TB, Reader<TR, TA>> f, TA a, IEnumerable<TB> bs) 
		// ReSharper restore InconsistentNaming
		{
			return (Reader<TR, Unit>)MonadCombinators<TR>.FoldMInternal_(f, a, bs, new ReaderMonadAdapter<TR>());
		}

		public static Reader<TR, IEnumerable<TC>> ZipWithM<TR, TA, TB, TC>(Func<TA, TB, Reader<TR, TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) 
		{
			return (Reader<TR, IEnumerable<TC>>)MonadCombinators<TR>.ZipWithMInternal(f, @as, bs, new ReaderMonadAdapter<TR>());
		}

		// ReSharper disable InconsistentNaming
		public static Reader<TR, Unit> ZipWithM_<TR, TA, TB, TC>(Func<TA, TB, Reader<TR, TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) 
		// ReSharper restore InconsistentNaming
		{
			return (Reader<TR, Unit>)MonadCombinators<TR>.ZipWithMInternal_(f, @as, bs, new ReaderMonadAdapter<TR>());
		}

		public static Reader<TR, IEnumerable<TA>> FilterM<TR, TA>(Func<TA, Reader<TR, bool>> p, IEnumerable<TA> @as) 
		{
			return (Reader<TR, IEnumerable<TA>>)MonadCombinators<TR>.FilterMInternal(p, @as, new ReaderMonadAdapter<TR>());
		}

		public static Reader<TR, Unit> When<TR>(bool b, Reader<TR, Unit> m) 
		{
			return m.When(b);
		}

		public static Reader<TR, Unit> When<TR>(this Reader<TR, Unit> m, bool b) 
		{
			return (Reader<TR, Unit>)MonadCombinators<TR>.When(b, m);
		}

		public static Reader<TR, Unit> Unless<TR>(bool b, Reader<TR, Unit> m) 
		{
			return m.Unless(b);
		}

		public static Reader<TR, Unit> Unless<TR>(this Reader<TR, Unit> m, bool b) 
		{
			return (Reader<TR, Unit>)MonadCombinators<TR>.Unless(b, m);
		}

		public static Reader<TR, TB> Forever<TR, TA, TB>(this Reader<TR, TA> m) 
		{
			return (Reader<TR, TB>)MonadCombinators<TR>.Forever<TA, TB>(m);
		}

		public static Reader<TR, Unit> Void<TR, TA>(this Reader<TR, TA> m) 
		{
			return (Reader<TR, Unit>)MonadCombinators<TR>.Void(m);
		}

		public static Reader<TR, TB> Ap<TR, TA, TB>(Reader<TR, Func<TA, TB>> mf, Reader<TR, TA> ma) 
		{
			return ma.Ap(mf);
		}

		public static Reader<TR, TB> Ap<TR, TA, TB>(this Reader<TR, TA> ma, Reader<TR, Func<TA, TB>> mf) 
		{
			return (Reader<TR, TB>)MonadCombinators<TR>.Ap(mf, ma);
		}

		public static Func<TA, Reader<TR, TC>> Compose<TR, TA, TB, TC>(Func<TA, Reader<TR, TB>> f, Func<TB, Reader<TR, TC>> g) 
		{
			return a => (Reader<TR, TC>)MonadCombinators<TR>.Compose(f, g)(a);
		}
	}

	public static partial class Writer
	{
		public static Writer<TMonoid, TMonoidAdapter, TW, TB> Bind<TMonoid, TMonoidAdapter, TW, TA, TB>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Func<TA, Writer<TMonoid, TMonoidAdapter, TW, TB>> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (Writer<TMonoid, TMonoidAdapter, TW, TB>)monadAdapter.Bind(ma, f);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TB> BindIgnoringLeft<TMonoid, TMonoidAdapter, TW, TA, TB>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, TB> mb) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			var monadAdapter = ma.GetMonadAdapter();
			return (Writer<TMonoid, TMonoidAdapter, TW, TB>)monadAdapter.BindIgnoringLeft(ma, mb);
		}

        public static Writer<TMonoid, TMonoidAdapter, TW, TB> Map<TMonoid, TMonoidAdapter, TW, TA, TB>(Func<TA, TB> f, Writer<TMonoid, TMonoidAdapter, TW, TA> ma) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
        {
            return ma.Map(f);
        }
		
        public static Writer<TMonoid, TMonoidAdapter, TW, TB> Map<TMonoid, TMonoidAdapter, TW, TA, TB>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Func<TA, TB> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
        {
            return ma.LiftM(f);
        }
		
        public static Writer<TMonoid, TMonoidAdapter, TW, TB> FlatMap<TMonoid, TMonoidAdapter, TW, TA, TB>(Func<TA, Writer<TMonoid, TMonoidAdapter, TW, TB>> f, Writer<TMonoid, TMonoidAdapter, TW, TA> ma) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
        {
            return ma.Bind(f);
        }

        public static Writer<TMonoid, TMonoidAdapter, TW, TB> FlatMap<TMonoid, TMonoidAdapter, TW, TA, TB>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Func<TA, Writer<TMonoid, TMonoidAdapter, TW, TB>> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
        {
            return ma.Bind(f);
        }
		
		public static Writer<TMonoid, TMonoidAdapter, TW, TB> LiftM<TMonoid, TMonoidAdapter, TW, TA, TB>(Func<TA, TB> f, Writer<TMonoid, TMonoidAdapter, TW, TA> ma) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return ma.LiftM(f);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TB> LiftM<TMonoid, TMonoidAdapter, TW, TA, TB>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Func<TA, TB> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, TB>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.LiftM(f, ma);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TC> LiftM2<TMonoid, TMonoidAdapter, TW, TA, TB, TC>(Func<TA, TB, TC> f, Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, TB> mb) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return ma.LiftM2(mb, f);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TC> LiftM2<TMonoid, TMonoidAdapter, TW, TA, TB, TC>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, TB> mb, Func<TA, TB, TC> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, TC>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.LiftM2(f, ma, mb);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TD> LiftM3<TMonoid, TMonoidAdapter, TW, TA, TB, TC, TD>(Func<TA, TB, TC, TD> f, Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, TB> mb, Writer<TMonoid, TMonoidAdapter, TW, TC> mc) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return ma.LiftM3(mb, mc, f);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TD> LiftM3<TMonoid, TMonoidAdapter, TW, TA, TB, TC, TD>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, TB> mb, Writer<TMonoid, TMonoidAdapter, TW, TC> mc, Func<TA, TB, TC, TD> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, TD>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.LiftM3(f, ma, mb, mc);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TE> LiftM4<TMonoid, TMonoidAdapter, TW, TA, TB, TC, TD, TE>(Func<TA, TB, TC, TD, TE> f, Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, TB> mb, Writer<TMonoid, TMonoidAdapter, TW, TC> mc, Writer<TMonoid, TMonoidAdapter, TW, TD> md) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return ma.LiftM4(mb, mc, md, f);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TE> LiftM4<TMonoid, TMonoidAdapter, TW, TA, TB, TC, TD, TE>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, TB> mb, Writer<TMonoid, TMonoidAdapter, TW, TC> mc, Writer<TMonoid, TMonoidAdapter, TW, TD> md, Func<TA, TB, TC, TD, TE> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, TE>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.LiftM4(f, ma, mb, mc, md);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TF> LiftM5<TMonoid, TMonoidAdapter, TW, TA, TB, TC, TD, TE, TF>(Func<TA, TB, TC, TD, TE, TF> f, Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, TB> mb, Writer<TMonoid, TMonoidAdapter, TW, TC> mc, Writer<TMonoid, TMonoidAdapter, TW, TD> md, Writer<TMonoid, TMonoidAdapter, TW, TE> me) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return ma.LiftM5(mb, mc, md, me, f);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TF> LiftM5<TMonoid, TMonoidAdapter, TW, TA, TB, TC, TD, TE, TF>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, TB> mb, Writer<TMonoid, TMonoidAdapter, TW, TC> mc, Writer<TMonoid, TMonoidAdapter, TW, TD> md, Writer<TMonoid, TMonoidAdapter, TW, TE> me, Func<TA, TB, TC, TD, TE, TF> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, TF>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.LiftM5(f, ma, mb, mc, md, me);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TA>> Sequence<TMonoid, TMonoidAdapter, TW, TA>(IEnumerable<Writer<TMonoid, TMonoidAdapter, TW, TA>> ms) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TA>>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.SequenceInternal(ms, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		// ReSharper disable InconsistentNaming
		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> Sequence_<TMonoid, TMonoidAdapter, TW, TA>(IEnumerable<Writer<TMonoid, TMonoidAdapter, TW, TA>> ms) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		// ReSharper restore InconsistentNaming
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, Unit>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.SequenceInternal_(ms, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TB>> MapM<TMonoid, TMonoidAdapter, TW, TA, TB>(Func<TA, Writer<TMonoid, TMonoidAdapter, TW, TB>> f, IEnumerable<TA> @as) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TB>>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.MapMInternal(f, @as, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		// ReSharper disable InconsistentNaming
		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> MapM_<TMonoid, TMonoidAdapter, TW, TA, TB>(Func<TA, Writer<TMonoid, TMonoidAdapter, TW, TB>> f, IEnumerable<TA> @as) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		// ReSharper restore InconsistentNaming
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, Unit>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.MapMInternal_(f, @as, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TB>> ForM<TMonoid, TMonoidAdapter, TW, TA, TB>(IEnumerable<TA> @as, Func<TA, Writer<TMonoid, TMonoidAdapter, TW, TB>> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TB>>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.MapMInternal(f, @as, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		// ReSharper disable InconsistentNaming
		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> ForM_<TMonoid, TMonoidAdapter, TW, TA, TB>(IEnumerable<TA> @as, Func<TA, Writer<TMonoid, TMonoidAdapter, TW, TB>> f) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		// ReSharper restore InconsistentNaming
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, Unit>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.MapMInternal_(f, @as, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TA>> ReplicateM<TMonoid, TMonoidAdapter, TW, TA>(int n, Writer<TMonoid, TMonoidAdapter, TW, TA> ma) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return ma.ReplicateM(n);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TA>> ReplicateM<TMonoid, TMonoidAdapter, TW, TA>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, int n) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TA>>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.ReplicateM(n, ma);
		}

		// ReSharper disable InconsistentNaming
		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> ReplicateM_<TMonoid, TMonoidAdapter, TW, TA>(int n, Writer<TMonoid, TMonoidAdapter, TW, TA> ma) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		// ReSharper restore InconsistentNaming
		{
			return ma.ReplicateM_(n);
		}

		// ReSharper disable InconsistentNaming
		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> ReplicateM_<TMonoid, TMonoidAdapter, TW, TA>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, int n) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		// ReSharper restore InconsistentNaming
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, Unit>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.ReplicateM_(n, ma);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TA> Join<TMonoid, TMonoidAdapter, TW, TA>(Writer<TMonoid, TMonoidAdapter, TW, Writer<TMonoid, TMonoidAdapter, TW, TA>> mma) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			// Ideally, we would like to use MonadCombinators<TMonoid, TMonoidAdapter, TW>.Join(mma) but there
			// is a casting issue that I have not figured out how to fix.
			var monadAdapter = mma.GetMonadAdapter();
			return (Writer<TMonoid, TMonoidAdapter, TW, TA>)monadAdapter.Bind(mma, MonadHelpers.Identity);
		}


		public static Writer<TMonoid, TMonoidAdapter, TW, TA> FoldM<TMonoid, TMonoidAdapter, TW, TA, TB>(Func<TA, TB, Writer<TMonoid, TMonoidAdapter, TW, TA>> f, TA a, IEnumerable<TB> bs) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, TA>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.FoldMInternal(f, a, bs, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		// ReSharper disable InconsistentNaming
		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> FoldM_<TMonoid, TMonoidAdapter, TW, TA, TB>(Func<TA, TB, Writer<TMonoid, TMonoidAdapter, TW, TA>> f, TA a, IEnumerable<TB> bs) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		// ReSharper restore InconsistentNaming
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, Unit>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.FoldMInternal_(f, a, bs, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TC>> ZipWithM<TMonoid, TMonoidAdapter, TW, TA, TB, TC>(Func<TA, TB, Writer<TMonoid, TMonoidAdapter, TW, TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TC>>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.ZipWithMInternal(f, @as, bs, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		// ReSharper disable InconsistentNaming
		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> ZipWithM_<TMonoid, TMonoidAdapter, TW, TA, TB, TC>(Func<TA, TB, Writer<TMonoid, TMonoidAdapter, TW, TC>> f, IEnumerable<TA> @as, IEnumerable<TB> bs) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		// ReSharper restore InconsistentNaming
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, Unit>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.ZipWithMInternal_(f, @as, bs, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TA>> FilterM<TMonoid, TMonoidAdapter, TW, TA>(Func<TA, Writer<TMonoid, TMonoidAdapter, TW, bool>> p, IEnumerable<TA> @as) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, IEnumerable<TA>>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.FilterMInternal(p, @as, new WriterMonadAdapter<TMonoid, TMonoidAdapter, TW>());
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> When<TMonoid, TMonoidAdapter, TW>(bool b, Writer<TMonoid, TMonoidAdapter, TW, Unit> m) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return m.When(b);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> When<TMonoid, TMonoidAdapter, TW>(this Writer<TMonoid, TMonoidAdapter, TW, Unit> m, bool b) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, Unit>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.When(b, m);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> Unless<TMonoid, TMonoidAdapter, TW>(bool b, Writer<TMonoid, TMonoidAdapter, TW, Unit> m) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return m.Unless(b);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> Unless<TMonoid, TMonoidAdapter, TW>(this Writer<TMonoid, TMonoidAdapter, TW, Unit> m, bool b) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, Unit>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.Unless(b, m);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TB> Forever<TMonoid, TMonoidAdapter, TW, TA, TB>(this Writer<TMonoid, TMonoidAdapter, TW, TA> m) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, TB>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.Forever<TA, TB>(m);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, Unit> Void<TMonoid, TMonoidAdapter, TW, TA>(this Writer<TMonoid, TMonoidAdapter, TW, TA> m) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, Unit>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.Void(m);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TB> Ap<TMonoid, TMonoidAdapter, TW, TA, TB>(Writer<TMonoid, TMonoidAdapter, TW, Func<TA, TB>> mf, Writer<TMonoid, TMonoidAdapter, TW, TA> ma) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return ma.Ap(mf);
		}

		public static Writer<TMonoid, TMonoidAdapter, TW, TB> Ap<TMonoid, TMonoidAdapter, TW, TA, TB>(this Writer<TMonoid, TMonoidAdapter, TW, TA> ma, Writer<TMonoid, TMonoidAdapter, TW, Func<TA, TB>> mf) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return (Writer<TMonoid, TMonoidAdapter, TW, TB>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.Ap(mf, ma);
		}

		public static Func<TA, Writer<TMonoid, TMonoidAdapter, TW, TC>> Compose<TMonoid, TMonoidAdapter, TW, TA, TB, TC>(Func<TA, Writer<TMonoid, TMonoidAdapter, TW, TB>> f, Func<TB, Writer<TMonoid, TMonoidAdapter, TW, TC>> g) where TMonoid : IMonoid<TW> where TMonoidAdapter : MonoidAdapter<TW>, new()
		{
			return a => (Writer<TMonoid, TMonoidAdapter, TW, TC>)MonadCombinators<TMonoid, TMonoidAdapter, TW>.Compose(f, g)(a);
		}
	}
}