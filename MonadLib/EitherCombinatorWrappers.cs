﻿using System;
using System.Collections.Generic;

namespace MonadLib
{
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

        public static Either<TLeft, TB> LiftM<TLeft, TA, TB>(this Either<TLeft, TA> ma, Func<TA, TB> f)
        {
            return (Either<TLeft, TB>)MonadCombinators<TLeft>.LiftM(f, ma);
        }

        public static Either<TLeft, TB> LiftM<TLeft, TA, TB>(Func<TA, TB> f, Either<TLeft, TA> ma)
        {
            return (Either<TLeft, TB>)MonadCombinators<TLeft>.LiftM(f, ma);
        }

        public static Either<TLeft, TC> LiftM2<TLeft, TA, TB, TC>(this Either<TLeft, TA> ma, Either<TLeft, TB> mb, Func<TA, TB, TC> f)
        {
            return (Either<TLeft, TC>)MonadCombinators<TLeft>.LiftM2(f, ma, mb);
        }

        public static Either<TLeft, TC> LiftM2<TLeft, TA, TB, TC>(Func<TA, TB, TC> f, Either<TLeft, TA> ma, Either<TLeft, TB> mb)
        {
            return (Either<TLeft, TC>)MonadCombinators<TLeft>.LiftM2(f, ma, mb);
        }

        public static Either<TLeft, TD> LiftM3<TLeft, TA, TB, TC, TD>(this Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Func<TA, TB, TC, TD> f)
        {
            return (Either<TLeft, TD>)MonadCombinators<TLeft>.LiftM3(f, ma, mb, mc);
        }

        public static Either<TLeft, TD> LiftM3<TLeft, TA, TB, TC, TD>(Func<TA, TB, TC, TD> f, Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc)
        {
            return (Either<TLeft, TD>)MonadCombinators<TLeft>.LiftM3(f, ma, mb, mc);
        }

        public static Either<TLeft, TE> LiftM4<TLeft, TA, TB, TC, TD, TE>(this Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Either<TLeft, TD> md, Func<TA, TB, TC, TD, TE> f)
        {
            return (Either<TLeft, TE>)MonadCombinators<TLeft>.LiftM4(f, ma, mb, mc, md);
        }

        public static Either<TLeft, TE> LiftM4<TLeft, TA, TB, TC, TD, TE>(Func<TA, TB, TC, TD, TE> f, Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Either<TLeft, TD> md)
        {
            return (Either<TLeft, TE>)MonadCombinators<TLeft>.LiftM4(f, ma, mb, mc, md);
        }

        public static Either<TLeft, TF> LiftM5<TLeft, TA, TB, TC, TD, TE, TF>(this Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Either<TLeft, TD> md, Either<TLeft, TE> me, Func<TA, TB, TC, TD, TE, TF> f)
        {
            return (Either<TLeft, TF>)MonadCombinators<TLeft>.LiftM5(f, ma, mb, mc, md, me);
        }

        public static Either<TLeft, TF> LiftM5<TLeft, TA, TB, TC, TD, TE, TF>(Func<TA, TB, TC, TD, TE, TF> f, Either<TLeft, TA> ma, Either<TLeft, TB> mb, Either<TLeft, TC> mc, Either<TLeft, TD> md, Either<TLeft, TE> me)
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
            return (Either<TLeft, IEnumerable<TA>>)MonadCombinators<TLeft>.ReplicateM(n, ma);
        }

        // ReSharper disable InconsistentNaming
        public static Either<TLeft, Unit> ReplicateM_<TLeft, TA>(int n, Either<TLeft, TA> ma)
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
            return (Either<TLeft, Unit>)MonadCombinators<TLeft>.When(b, m);
        }

        public static Either<TLeft, Unit> Unless<TLeft>(bool b, Either<TLeft, Unit> m)
        {
            return (Either<TLeft, Unit>)MonadCombinators<TLeft>.Unless(b, m);
        }

        public static Either<TLeft, TB> Forever<TLeft, TA, TB>(Either<TLeft, TA> m)
        {
            return (Either<TLeft, TB>)MonadCombinators<TLeft>.Forever<TA, TB>(m);
        }

        public static Either<TLeft, Unit> Void<TLeft, TA>(Either<TLeft, TA> m)
        {
            return (Either<TLeft, Unit>)MonadCombinators<TLeft>.Void(m);
        }

        public static Either<TLeft, TB> Ap<TLeft, TA, TB>(Either<TLeft, Func<TA, TB>> mf, Either<TLeft, TA> ma)
        {
            return (Either<TLeft, TB>)MonadCombinators<TLeft>.Ap(mf, ma);
        }
    }
}