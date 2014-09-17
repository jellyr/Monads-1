﻿using System;

namespace MonadLib
{
    public sealed class Either<TE, TA> : IMonad<TE, TA>
    {
        private Either(LeftOrRight leftOrRight, TE e, TA a)
        {
            _leftOrRight = leftOrRight;
            _e = e;
            _a = a;
        }

        internal Either(TE e)
            : this(LeftOrRight.Left, e, default(TA))
        {
        }

        internal Either(TA a)
            : this(LeftOrRight.Right, default(TE), a)
        {
        }

        public bool IsLeft {
            get { return _leftOrRight == LeftOrRight.Left; }
        }

        public bool IsRight {
            get { return _leftOrRight == LeftOrRight.Right; }
        }

        public TE Left
        {
            get
            {
                if (!IsLeft) throw new InvalidOperationException("Left called on Either that is Right.");
                return _e;
            }
        }

        public TA Right
        {
            get
            {
                if (!IsRight) throw new InvalidOperationException("Right called on Either that is Left.");
                return _a;
            }
        }

        private enum LeftOrRight
        {
            Left,
            Right
        }

        private readonly LeftOrRight _leftOrRight;
        private readonly TE _e;
        private readonly TA _a;

        private IMonadAdapter<TE> _monadAdapter;

        public IMonadAdapter<TE> GetMonadAdapter()
        {
            return _monadAdapter ?? (_monadAdapter = new EitherMonadAdapter<TE>());
        }
    }

    public static class Either
    {
        public static Either<TE, TB> Bind<TE, TA, TB>(this Either<TE, TA> ma, Func<TA, Either<TE, TB>> f)
        {
            var monadAdapter = ma.GetMonadAdapter();
            return (Either<TE, TB>)monadAdapter.Bind(ma, f);
        }

        public static Either<TE, TB> LiftM<TE, TA, TB>(this Either<TE, TA> ma, Func<TA, TB> f)
        {
            return (Either<TE, TB>)MonadCombinators<TE>.LiftM(f, ma);
        }

        public static Either<TE, TB> LiftM<TE, TA, TB>(Func<TA, TB> f, Either<TE, TA> ma)
        {
            return (Either<TE, TB>)MonadCombinators<TE>.LiftM(f, ma);
        }
    }

    public static class Either<TE>
    {
        public static Either<TE, TA> Left<TA>(TE e)
        {
            return new Either<TE, TA>(e);
        }

        public static Either<TE, TA> Right<TA>(TA a)
        {
            return new Either<TE, TA>(a);
        }

        public static Either<TE, TA> Unit<TA>(TA a)
        {
            return Right(a);
        }
    }

    internal class EitherMonadAdapter<TE> : IMonadAdapter<TE>
    {
        public IMonad<TE, TA> Unit<TA>(TA a)
        {
            return Either<TE>.Right(a);
        }

        public IMonad<TE, TB> Bind<TA, TB>(IMonad<TE, TA> ma, Func<TA, IMonad<TE, TB>> f)
        {
            var eitherA = (Either<TE, TA>)ma;
            return eitherA.IsRight ? f(eitherA.Right) : Either<TE>.Left<TB>(eitherA.Left);
        }
    }
}
