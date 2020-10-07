// maybe have class PredictableObj which wraps roundObject

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game1
{
    public class Predictor
    {
        private readonly List<RoundObject> predictableObjs;
        private readonly int playerInd;
        private readonly Image predImage;
        private readonly List<RoundObject> objPreds;
        private readonly List<List<Vector2>> predPosss;
        private RoundObject playerPred;
        private bool playerPredDies;
        private List<Vector2> playerPredPoss;

        public Predictor(List<RoundObject> predictableObjs, int playerInd)
        {
            this.predictableObjs = predictableObjs;
            this.playerInd = playerInd;
            predImage = new Image(imageName: "disk", width: 3, color: Color.White * .5f);
            objPreds = new List<RoundObject>();
            predPosss = new List<List<Vector2>>();
        }

        public void Update(int frames, float elapsed)
        {
            objPreds.Clear();
            predPosss.Clear();
            playerPredDies = false;
            foreach (RoundObject predictableObj in predictableObjs)
            {
                objPreds.Add(predictableObj.CloneForPred());
                predPosss.Add(new List<Vector2>());
            }

            playerPred = objPreds[playerInd];
            playerPredPoss = new List<Vector2>();
            
            for (int i = 0; i < frames && !playerPredDies; i++)
            {
                Update(elapsed);

                Vector2 adjustPos = Vector2.Zero;
                float coeffSum = 0;
                for (int j = 0; j < objPreds.Count; j++)
                    if (j != playerInd)
                    {
                        float coeff = 1 / Vector2.DistanceSquared(objPreds[j].Position, objPreds[playerInd].Position);
                        adjustPos += coeff * (predictableObjs[j].Position - objPreds[j].Position);
                        coeffSum += coeff;
                    }
                if (coeffSum != 0)
                    adjustPos /= coeffSum;
                playerPredPoss.Add(playerPred.Position + adjustPos);
                foreach ((RoundObject objPred, List<Vector2> predPoss) in objPreds.Zip(predPosss, (o, p) => (o, p)))
                    predPoss.Add(objPred.Position + adjustPos);
            }

        }

        private void Update(float elapsed)
        {
            foreach (RoundObject objPred in objPreds)
                foreach (RoundObject otherObjPred in objPreds)
                    objPred.GravityPull(otherObjPred);

            foreach (RoundObject objPred in objPreds)
                objPred.Update(elapsed);
            
            foreach (RoundObject objPred in objPreds)
                if (playerPred.Intersects(objPred))
                {
                    playerPredDies = true;
                    break;
                }
        }

        public void Draw()
        {
            foreach (Vector2 playerPredPos in playerPredPoss)
                predImage.Draw(playerPredPos);
            //foreach (List<Vector2> predPoss in predPosss)
            //    foreach (Vector2 predPos in predPoss)
            //        predImage.Draw(predPos);
        }
    }
}
