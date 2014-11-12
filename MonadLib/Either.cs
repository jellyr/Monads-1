﻿using System;
using System.Collections.Generic;
using System.Linq;
using Flinq;

namespace MonadLib
{
    public sealed class Either<TLeft, TA> : IMonad<TLeft, TA>
    {
        private Either(LeftOrRight leftOrRight, TLeft left, TA right)
        {
            _leftOrRight = leftOrRight;
            _left = left;
            _right = right;
        }

        internal Either(TLeft left)
            : this(LeftOrRight.Left, left, default(TA))
        {
        }

        internal Either(TA right)
            : this(LeftOrRight.Right, default(TLeft), right)
        {
        }

        public bool IsLeft {
            get { return _leftOrRight == LeftOrRight.Left; }
        }

        public bool IsRight {
            get { return _leftOrRight == LeftOrRight.Right; }
        }

        public TLeft Left
        {
            get
            {
                if (!IsLeft) throw new InvalidOperationException("Left called on Either that is Right.");
                return _left;
            }
        }

        public TA Right
        {
            get
            {
                if (!IsRight) throw new InvalidOperationException("Right called on Either that is Left.");
                return _right;
            }
        }

        public void Match(Action<TLeft> leftAction, Action<TA> rightAction)
        {
            if (IsLeft)
                leftAction(Left);
            else
                rightAction(Right);
        }

        public T Match<T>(Func<TLeft, T> leftFunc, Func<TA, T> rightFunc)
        {
            return IsLeft ? leftFunc(Left) : rightFunc(Right);
        }

        private enum LeftOrRight
        {
            Left,
            Right
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var other = obj as Either<TLeft, TA>;
            if (other == null) return false;

            if (IsLeft && other.IsLeft)
            {
                return EqualityComparer<TLeft>.Default.Equals(Left, other.Left);
            }

            if (IsRight && other.IsRight)
            {
                return EqualityComparer<TA>.Default.Equals(Right, other.Right);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Match(l => l.GetHashCode(), r  => r.GetHashCode());
        }

        private readonly LeftOrRight _leftOrRight;
        private readonly TLeft _left;
        private readonly TA _right;

        private MonadAdapter<TLeft> _monadAdapter;

        public MonadAdapter<TLeft> GetMonadAdapter()
        {
            return _monadAdapter ?? (_monadAdapter = new EitherMonadAdapter<TLeft>());
        }
    }

    public static partial class Either
    {
        public static IEnumerable<TLeft> Lefts<TLeft, TA>(IEnumerable<Either<TLeft, TA>> eithers)
        {
            return eithers.Where(e => e.IsLeft).Select(e => e.Left);
        }

        public static IEnumerable<TA> Rights<TLeft, TA>(IEnumerable<Either<TLeft, TA>> eithers)
        {
            return eithers.Where(e => e.IsRight).Select(e => e.Right);
        }

        public static Tuple<IEnumerable<TLeft>, IEnumerable<TA>> PartitionEithers<TLeft, TA>(IEnumerable<Either<TLeft, TA>> eithers)
        {
            var z = Tuple.Create(MonadHelpers.Nil<TLeft>(), MonadHelpers.Nil<TA>());

            return eithers.FoldRight(z, (either, acc) => either.Match(
                left => Tuple.Create(MonadHelpers.Cons(left, acc.Item1), acc.Item2),
                right => Tuple.Create(acc.Item1, MonadHelpers.Cons(right, acc.Item2))));
        }

        public static TB MapEither<TLeft, TA, TB>(Func<TLeft, TB> f, Func<TA, TB> g, Either<TLeft, TA> either)
        {
            return either.Match(f, g);
        }

        public static TB MapEither<TLeft, TA, TB>(this Either<TLeft, TA> either, Func<TLeft, TB> f, Func<TA, TB> g)
        {
            return either.Match(f, g);
        }
    }

    public static class Either<TLeft>
    {
        public static Either<TLeft, TA> Left<TA>(TLeft left)
        {
            return new Either<TLeft, TA>(left);
        }

        public static Either<TLeft, TA> Right<TA>(TA right)
        {
            return new Either<TLeft, TA>(right);
        }

        public static Either<TLeft, TA> Return<TA>(TA right)
        {
            return Right(right);
        }
    }

    internal class EitherMonadAdapter<TLeft> : MonadAdapter<TLeft>
    {
        public override IMonad<TLeft, TA> Return<TA>(TA right)
        {
            return Either<TLeft>.Right(right);
        }

        public override IMonad<TLeft, TB> Bind<TA, TB>(IMonad<TLeft, TA> ma, Func<TA, IMonad<TLeft, TB>> f)
        {
            var eitherA = (Either<TLeft, TA>)ma;
            return eitherA.IsRight ? f(eitherA.Right) : Either<TLeft>.Left<TB>(eitherA.Left);
        }
    }
}
