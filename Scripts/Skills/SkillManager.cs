using UnityEngine;

public class SkillManager : MonoBehaviour {
    public Skills[] skills;

    private void Start() {
        skills = new Skills[] {
            new() { skillName = "Пронзающий удар", isActive = false}
        };
    }
    
    public void ChooseSkill(int skillIndex) {
        if (skillIndex < 0 || skillIndex >= skills.Length) {
            Debug.Log("Некорректный индекс навыка");   
        }

        Skills skillToChoose = skills[skillIndex];

        if (skillToChoose.isActive) {
            skillToChoose.Activate();
            Debug.Log("Аугментация " + skillToChoose.skillName + "был активирован");
        } else if (skillToChoose.isActive) {
            Debug.Log("Аугментация уже активирована");
        }
    }
    public void Attack() {
        foreach (Skills skill in skills) {
            if (skill is ExtendedTrailSkill extendedTrailSkill && extendedTrailSkill.isActive) {
               // extendedTrailSkill.ApplyExtendedTrail(hitObject);
            } 
        }
    }
}
