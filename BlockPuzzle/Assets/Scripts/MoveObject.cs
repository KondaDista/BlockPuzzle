using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public bool isMoving;

    private Vector3 resetPosition;
    public GameObject target;

    void Update()
    {
        if (isMoving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos= Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.position = new Vector3(mousePos.x , mousePos.y + 0.5f, this.transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        isMoving = true;
        resetPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    private void OnMouseUp()
    {
        isMoving = false;
        if(Mathf.Abs(this.transform.position.x - target.transform.position.x) <= 1.5f && Mathf.Abs(this.transform.position.y - target.transform.position.y) <= 1.5f)
        {
            if (target.CompareTag("TablePocket") && target.GetComponent<TablePoket>().busy == false)
            {
                target.GetComponent<TablePoket>().busy = true;
                this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
                this.transform.parent = target.transform;

                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>().CheckIfComletedLine();
                this.enabled = false;
            }
            else if(target.CompareTag("TablePocket") && target.GetComponent<TablePoket>().busy == true)
            {
                this.transform.position = resetPosition;
            }
            else
            {
                this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            }
        }
        else
        {
            this.transform.position = resetPosition;
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if(isMoving)
        {
            target = col.gameObject;
        }
    }
}
