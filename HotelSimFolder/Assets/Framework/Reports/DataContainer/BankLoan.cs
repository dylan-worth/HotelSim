using UnityEngine;


[System.Serializable]
public class BankLoan {

    public Date StartingDate;//date the loan was taken.

    public int duration;//duration in months.
    public int remainingDuration;
    public float amount;
    public float interestRate;
    public float monthlyInstallment;//monthly cost.

    public string reason;//reason to take up the loan. Player will be prompt before completing the loan.

    public BankLoan() { }
    public BankLoan(Date startD, int length, float sum, float rate, string text)
    {
        StartingDate = startD;
        duration = length;
        amount = sum;
        interestRate = rate;
        reason = text;
        remainingDuration = 0;
    }

    public void SetInstallment()
    {
        monthlyInstallment = (amount * (interestRate / 12f)) / (1 - Mathf.Pow((1 + (interestRate / 12f)), -duration));
    }

}
