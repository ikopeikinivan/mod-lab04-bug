using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugPro;

namespace BugTests;

[TestClass]
public class BugStateMachineTests
{
    [TestMethod]
    public void TestInitialState_ShouldBeNewDefect()
    {
        var bug = new Bug();
        Assert.AreEqual(BugState.NewDefect, bug.State);
    }

    [TestMethod]
    public void TestAssignToTriage_TransitionsToTriage()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        Assert.AreEqual(BugState.Triage, bug.State);
    }

    [TestMethod]
    public void TestTriageToFixing_ValidTransition()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        bug.TriageFix();
        Assert.AreEqual(BugState.Fixing, bug.State);
    }

    [TestMethod]
    public void TestFixingToProblemSolved_ValidTransition()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        bug.TriageFix();
        bug.FixCompleted();
        Assert.AreEqual(BugState.ProblemSolved, bug.State);
    }

    [TestMethod]
    public void TestProblemSolvedToClosed_ValidTransition()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        bug.TriageFix();
        bug.FixCompleted();
        bug.VerifyYes();
        Assert.AreEqual(BugState.Closed, bug.State);
    }

    [TestMethod]
    public void TestProblemSolvedToReopened_ValidTransition()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        bug.TriageFix();
        bug.FixCompleted();
        bug.VerifyNo();
        Assert.AreEqual(BugState.Reopened, bug.State);
    }

    [TestMethod]
    public void TestReopenedToFixing_ValidTransition()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        bug.TriageFix();
        bug.FixCompleted();
        bug.VerifyNo();
        bug.ReopenFix();
        Assert.AreEqual(BugState.Fixing, bug.State);
    }

    [TestMethod]
    public void TestTriageToNotDefectAndClose()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        bug.TriageNotDefect();
        Assert.AreEqual(BugState.NotDefect, bug.State);
        bug.CloseNotDefect();
        Assert.AreEqual(BugState.Closed, bug.State);
    }

    [TestMethod]
    public void TestTriageToDuplicateAndClose()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        bug.TriageDuplicate();
        Assert.AreEqual(BugState.Duplicate, bug.State);
        bug.CloseDuplicate();
        Assert.AreEqual(BugState.Closed, bug.State);
    }

    [TestMethod]
    public void TestNeedMoreInfo_ProvideInfo_ReturnsToTriage()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        bug.TriageNeedMoreInfo();
        Assert.AreEqual(BugState.NeedMoreInfo, bug.State);
        bug.ProvideMoreInfo();
        Assert.AreEqual(BugState.Triage, bug.State);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestInvalidTransition_FromNewDefectToClosed_Throws()
    {
        var bug = new Bug();
        bug.CloseNotDefect(); 
    }

    [TestMethod]
    public void TestFullHappyPath_NewToClosed()
    {
        var bug = new Bug();
        bug.AssignToTriage();
        bug.TriageFix();
        bug.FixCompleted();
        bug.VerifyYes();
        Assert.AreEqual(BugState.Closed, bug.State);
    }
}