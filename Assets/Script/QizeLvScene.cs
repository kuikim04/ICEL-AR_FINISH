using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    [CreateAssetMenu(fileName = "DataChoice", menuName = "DataChoice/QnA", order = 2)]
    public class QizeLvScene : ScriptableObject
    {
        public List<QnA> QnA;
    }
}
