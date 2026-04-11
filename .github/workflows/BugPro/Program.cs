using Stateless;

namespace BugPro;

public enum BugState
{
    NewDefect,              
    Triage,                
    NoTimeNow,              
    NeedSeparateSolution,   
    OtherProductProblem,    
    NeedMoreInfo,           
    NotDefect,              
    DontFix,                
    Duplicate,              
    NotReproducible,       
    Fixing,                 
    ProblemSolved,          
    Closed,                 
    Reopened                
}

public enum BugTrigger
{
    AssignToTriage,
    TriageNoTimeNow,
    TriageSeparateSolution,
    TriageOtherProduct,
    TriageNeedMoreInfo,
    TriageNotDefect,
    TriageDontFix,
    TriageDuplicate,
    TriageNotReproducible,
    TriageFix,
    ProvideMoreInfo,
    CloseNotDefect,
    CloseDontFix,
    CloseDuplicate,
    CloseNotReproducible,
    CloseNoTimeNow,
    CloseSeparateSolution,
    CloseOtherProduct,
    FixCompleted,
    VerifyYes,
    VerifyNo,
    ReopenFix
}

public class Bug
{
    private readonly StateMachine<BugState, BugTrigger> _machine;

    public BugState State => _machine.State;

    public Bug()
    {
        _machine = new StateMachine<BugState, BugTrigger>(BugState.NewDefect);

        // Конфигурация автомата
        _machine.Configure(BugState.NewDefect)
            .Permit(BugTrigger.AssignToTriage, BugState.Triage);

        _machine.Configure(BugState.Triage)
            .Permit(BugTrigger.TriageNoTimeNow, BugState.NoTimeNow)
            .Permit(BugTrigger.TriageSeparateSolution, BugState.NeedSeparateSolution)
            .Permit(BugTrigger.TriageOtherProduct, BugState.OtherProductProblem)
            .Permit(BugTrigger.TriageNeedMoreInfo, BugState.NeedMoreInfo)
            .Permit(BugTrigger.TriageNotDefect, BugState.NotDefect)
            .Permit(BugTrigger.TriageDontFix, BugState.DontFix)
            .Permit(BugTrigger.TriageDuplicate, BugState.Duplicate)
            .Permit(BugTrigger.TriageNotReproducible, BugState.NotReproducible)
            .Permit(BugTrigger.TriageFix, BugState.Fixing);

        _machine.Configure(BugState.NoTimeNow)
            .Permit(BugTrigger.CloseNoTimeNow, BugState.Closed);

        _machine.Configure(BugState.NeedSeparateSolution)
            .Permit(BugTrigger.CloseSeparateSolution, BugState.Closed);

        _machine.Configure(BugState.OtherProductProblem)
            .Permit(BugTrigger.CloseOtherProduct, BugState.Closed);

        _machine.Configure(BugState.NeedMoreInfo)
            .Permit(BugTrigger.ProvideMoreInfo, BugState.Triage);

        _machine.Configure(BugState.NotDefect)
            .Permit(BugTrigger.CloseNotDefect, BugState.Closed);

        _machine.Configure(BugState.DontFix)
            .Permit(BugTrigger.CloseDontFix, BugState.Closed);

        _machine.Configure(BugState.Duplicate)
            .Permit(BugTrigger.CloseDuplicate, BugState.Closed);

        _machine.Configure(BugState.NotReproducible)
            .Permit(BugTrigger.CloseNotReproducible, BugState.Closed);

        _machine.Configure(BugState.Fixing)
            .Permit(BugTrigger.FixCompleted, BugState.ProblemSolved);

        _machine.Configure(BugState.ProblemSolved)
            .Permit(BugTrigger.VerifyYes, BugState.Closed)
            .Permit(BugTrigger.VerifyNo, BugState.Reopened);

        _machine.Configure(BugState.Reopened)
            .Permit(BugTrigger.ReopenFix, BugState.Fixing);
    }

    public void AssignToTriage() => _machine.Fire(BugTrigger.AssignToTriage);
    public void TriageNoTimeNow() => _machine.Fire(BugTrigger.TriageNoTimeNow);
    public void TriageSeparateSolution() => _machine.Fire(BugTrigger.TriageSeparateSolution);
    public void TriageOtherProduct() => _machine.Fire(BugTrigger.TriageOtherProduct);
    public void TriageNeedMoreInfo() => _machine.Fire(BugTrigger.TriageNeedMoreInfo);
    public void TriageNotDefect() => _machine.Fire(BugTrigger.TriageNotDefect);
    public void TriageDontFix() => _machine.Fire(BugTrigger.TriageDontFix);
    public void TriageDuplicate() => _machine.Fire(BugTrigger.TriageDuplicate);
    public void TriageNotReproducible() => _machine.Fire(BugTrigger.TriageNotReproducible);
    public void TriageFix() => _machine.Fire(BugTrigger.TriageFix);
    public void ProvideMoreInfo() => _machine.Fire(BugTrigger.ProvideMoreInfo);
    public void CloseNotDefect() => _machine.Fire(BugTrigger.CloseNotDefect);
    public void CloseDontFix() => _machine.Fire(BugTrigger.CloseDontFix);
    public void CloseDuplicate() => _machine.Fire(BugTrigger.CloseDuplicate);
    public void CloseNotReproducible() => _machine.Fire(BugTrigger.CloseNotReproducible);
    public void CloseNoTimeNow() => _machine.Fire(BugTrigger.CloseNoTimeNow);
    public void CloseSeparateSolution() => _machine.Fire(BugTrigger.CloseSeparateSolution);
    public void CloseOtherProduct() => _machine.Fire(BugTrigger.CloseOtherProduct);
    public void FixCompleted() => _machine.Fire(BugTrigger.FixCompleted);
    public void VerifyYes() => _machine.Fire(BugTrigger.VerifyYes);
    public void VerifyNo() => _machine.Fire(BugTrigger.VerifyNo);
    public void ReopenFix() => _machine.Fire(BugTrigger.ReopenFix);

    public string GetStateName() => State switch
    {
        BugState.NewDefect => "НОВЫЙ ДЕФЕКТ",
        BugState.Triage => "РАЗБОР ДЕФЕКТОВ",
        BugState.NoTimeNow => "НЕТ ВРЕМЕНИ СЕЙЧАС",
        BugState.NeedSeparateSolution => "НУЖНО ОТДЕЛЬНОЕ РЕШЕНИЕ",
        BugState.OtherProductProblem => "ПРОБЛЕМА ДРУГОГО ПРОДУКТА",
        BugState.NeedMoreInfo => "НУЖНО БОЛЬШЕ ИНФОРМАЦИИ",
        BugState.NotDefect => "НЕ ДЕФЕКТ",
        BugState.DontFix => "НЕ ИСПРАВЛЯТЬ",
        BugState.Duplicate => "ДУБЛИРОВАНИЕ",
        BugState.NotReproducible => "НЕ ВОСПРОИЗВОДИМО",
        BugState.Fixing => "ИСПРАВЛЕНИЕ",
        BugState.ProblemSolved => "ПРОБЛЕМА РЕШЕНА?",
        BugState.Closed => "ЗАКРЫТИЕ",
        BugState.Reopened => "ПЕРЕОТКРЫТИЕ",
        _ => "НЕИЗВЕСТНО"
    };
}

public class Program
{
    public static void Main()
    {
        var bug = new Bug();
        Console.WriteLine($"Начальное состояние: {bug.GetStateName()}");

        bug.AssignToTriage();
        Console.WriteLine($"После передачи на разбор: {bug.GetStateName()}");

        bug.TriageFix();
        Console.WriteLine($"После назначения на исправление: {bug.GetStateName()}");

        bug.FixCompleted();
        Console.WriteLine($"После исправления: {bug.GetStateName()}");

        bug.VerifyYes();
        Console.WriteLine($"После подтверждения закрытия: {bug.GetStateName()}");

        Console.WriteLine("\nРабочий процесс завершён.");
    }
}