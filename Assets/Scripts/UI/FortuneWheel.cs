using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FortuneWheel : MonoBehaviour
{
    public float speed = 3;
    public List<RewardType> prize;
    public List<AnimationCurve> animationCurves;

    private bool spinning;
    private float anglePerItem;
    private int randomTime;
    private int itemNumber;

    void Start()
    {
        spinning = false;
        anglePerItem = 360 / prize.Count;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !spinning)
        {
            StartSpinning();
        }
    }

    public void StartSpinning()
    {
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
        randomTime = Random.Range(2, 4);
        itemNumber = Random.Range(0, prize.Count);
        float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);

        StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
    }

    IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        spinning = true;
        SoundManager.instance.Play("FortuneWheel");
        float timer = 0.0f;
        float startAngle = transform.eulerAngles.z;
        maxAngle = maxAngle - startAngle;

        int animationCurveNumber = Random.Range(0, animationCurves.Count);
        Debug.Log("Animation Curve No. : " + animationCurveNumber);

        while (timer < time)
        {
            //to calculate rotation
            float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime * speed;
            yield return 0;
        }
        SoundManager.instance.Stop("FortuneWheel");
        transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        spinning = false;
        RewardScreen.OpenRewardAction(prize[itemNumber]);
    }

    public enum RewardType
    {
        Nitrous1,
        Nitrous2,
        Nitrous3,
        Attraction1,
        Attraction2,
        Attraction3,
        Shield1,
        Shield2,
        Shield3,
        CarSkin,
        Gold50,
        Gold100,
        Gold300,
        Gold500,
        Gold1000
    }
}

public abstract class RewardItem
{
    public Sprite rewardSprite;
    public abstract void Init();
    public abstract void ClaimReward();
}

public abstract class BoostReward : RewardItem
{
    public int count;
}

public class NitrosReward : BoostReward
{
    public override void ClaimReward()
    {
        Debug.Log("NitrosReward Init" );
    }

    public override void Init()
    {
        Debug.Log("Nitrols init");
    }
}

public class AttractionReward : BoostReward
{
    public override void ClaimReward()
    {
        throw new System.NotImplementedException();
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }
}


public class DivineShieldReward : BoostReward
{
    public override void ClaimReward()
    {
        throw new System.NotImplementedException();
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }
}

public class CarReward : RewardItem
{
    public CarItem[] cars;

    public override void ClaimReward()
    {
        throw new System.NotImplementedException();
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }
}

public class CoinReward
{
    public int number;
}