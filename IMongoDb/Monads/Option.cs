namespace IMongoDb.Monads;

public class Option<TSomeValueType>
{
	public Option()
	{
		value = default;
	}

	public static Option<TSomeValueType> Some(TSomeValueType value)
	{
		return new Option<TSomeValueType>(value);
	}

	public static Option<TSomeValueType> None()
	{
		return new Option<TSomeValueType>();
	}

	public  Option<TReturnType> Map<TReturnType>(Func<TSomeValueType, TReturnType> optionMapper)
	{
		return value is not null ? new Option<TReturnType>(optionMapper.Invoke(value))
			: new Option<TReturnType>();
	}

	public TOutType MapExpression<TOutType>(Func<TOutType> someMapper, Func<TOutType> noneMapper)
	{
		return value is not null ? someMapper.Invoke()
			: noneMapper.Invoke();
	}

	public OutType MapExpression<OutType>(Func<TSomeValueType, OutType> someMapper, Func<OutType> noneMapper)
	{
		return value is not null ? someMapper.Invoke(value)
			: noneMapper.Invoke();
	}

	public Option<OutOptionalType> AndThen<OutOptionalType>(Func<TSomeValueType, Option<OutOptionalType>> optionMapper)
	{
		var nestedOption = Map(optionMapper);

		return nestedOption.IsSet() && (nestedOption.GetValue().IsSet()) ? Option<OutOptionalType>.Some(nestedOption.GetValue().GetValue())
			: Option<OutOptionalType>.None();
	}

	public void MatchSome(Action<TSomeValueType> someFunctor)
	{
		if (IsSet())
		{
			someFunctor.Invoke(value);
		}
	}

	public void MatchNone(Action noneFunctor)
	{
		if (!IsSet())
		{
			noneFunctor.Invoke();
		}
	}

	public void Match(Action<TSomeValueType> someFunctor, Action noneFunctor)
	{
		if (IsSet())
		{
			someFunctor.Invoke(value);
		}
		else
		{
			noneFunctor.Invoke();
		}
	}

	public ref TSomeValueType GetValue()
	{
		if (value is null)
		{
			throw new NullReferenceException("Tried to access a Option's value while it was empty");
		}
		return ref value!;
	}

	public bool IsSet()
	{
		return value is not null;
	}

	public bool IsEmpty()
	{
		return value is null;
	}

	public TSomeValueType GetValueOr(TSomeValueType defaultValue)
	{
		return  value ?? defaultValue;
	}

	private Option(TSomeValueType value)
	{
		this.value = value;
	}

	private TSomeValueType? value;
}