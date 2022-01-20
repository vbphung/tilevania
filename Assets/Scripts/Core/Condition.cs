using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class Condition
    {
        [SerializeField] Disjucntion[] and;

        public bool IsSatisfied(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (Disjucntion disjucntion in and)
                if (!disjucntion.IsSatisfied(evaluators))
                    return false;
            return true;
        }

        [System.Serializable]
        class Disjucntion
        {
            [SerializeField] Predicate[] or;

            public bool IsSatisfied(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (Predicate predicate in or)
                    if (predicate.IsSatisfied(evaluators))
                        return true;
                return false;
            }
        }

        [System.Serializable]
        class Predicate
        {
            [SerializeField] string predicate;
            [SerializeField] string[] parameters;
            [SerializeField] bool negate = false;

            public bool IsSatisfied(IEnumerable<IPredicateEvaluator> evaluators)
            {
                foreach (IPredicateEvaluator evaluator in evaluators)
                {
                    bool? result = evaluator.Evaluate(predicate, parameters);
                    if (result == null)
                        continue;
                    if (result == negate)
                        return false;
                }
                return true;
            }
        }
    }
}