using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class mixColors : MonoBehaviour
{
  public GameObject mainCube;
  public GameObject colorCube1;
  public GameObject colorCube2;
  public GameObject colorCube3;
  public GameObject whiteCube;
  public GameObject menu;

  void Start()
  {
    MeshRenderer colorCube1 = this.colorCube1.GetComponent<MeshRenderer>();
    MeshRenderer colorCube2 = this.colorCube2.GetComponent<MeshRenderer>();
    MeshRenderer colorCube3 = this.colorCube3.GetComponent<MeshRenderer>();
    MeshRenderer whiteCube = this.whiteCube.GetComponent<MeshRenderer>();
    colorCube1.material.color = Color.red;
    colorCube2.material.color = Color.blue;
    colorCube3.material.color = Color.green;
    whiteCube.material.color = Color.white;
    menu.SetActive(false);
  }

  void OnTriggerEnter(Collider other)
  {
    MeshRenderer mainCubeMeshRend = mainCube.GetComponent<MeshRenderer>();
    MeshRenderer colorCube = other.GetComponent<MeshRenderer>();

    if (other.gameObject.CompareTag("CubeRed"))
    {
      if (mainCubeMeshRend.material.color == Color.white)
      {
        mainCubeMeshRend.material.color = colorCube.material.color;
      }
      else
      {
        var r = mainCubeMeshRend.material.color.r + colorCube.material.color.r;
        var g = mainCubeMeshRend.material.color.g + colorCube.material.color.g;
        var b = mainCubeMeshRend.material.color.b + colorCube.material.color.b;
        r /= 2;
        g /= 2;
        b /= 2;
        var mixedColor = new Color(r, g, b, 1);
        mainCubeMeshRend.material.color = mixedColor;

        if (mainCubeMeshRend.material.color.r == 0.5f && mainCubeMeshRend.material.color.g == 0.5f && mainCubeMeshRend.material.color.b == 0)
        {
          menu.gameObject.SetActive(true);
        }
      }
    }

    if (other.gameObject.CompareTag("CubeGreen"))
    {
      if (mainCubeMeshRend.material.color == Color.white)
      {
        mainCubeMeshRend.material.color = colorCube.material.color;
      }
      else
      {
        var r = mainCubeMeshRend.material.color.r + colorCube.material.color.r;
        var g = mainCubeMeshRend.material.color.g + colorCube.material.color.g;
        var b = mainCubeMeshRend.material.color.b + colorCube.material.color.b;
        r /= 2;
        g /= 2;
        b /= 2;
        var mixedColor = new Color(r, g, b, 1);
        mainCubeMeshRend.material.color = mixedColor;

        if (mainCubeMeshRend.material.color.r == 0.5f && mainCubeMeshRend.material.color.g == 0.5f && mainCubeMeshRend.material.color.b == 0)
        {
          menu.gameObject.SetActive(true);
        }
      }
    }

    if (other.gameObject.CompareTag("CubeBlue"))
    {
      if (mainCubeMeshRend.material.color == Color.white)
      {
        mainCubeMeshRend.material.color = colorCube.material.color;
      }
      else
      {
        var r = mainCubeMeshRend.material.color.r + colorCube.material.color.r;
        var g = mainCubeMeshRend.material.color.g + colorCube.material.color.g;
        var b = mainCubeMeshRend.material.color.b + colorCube.material.color.b;
        r /= 2;
        g /= 2;
        b /= 2;
        var mixedColor = new Color(r, g, b, 1);
        mainCubeMeshRend.material.color = mixedColor;
      }
    }

    if (other.gameObject.CompareTag("CubeWhite"))
    {
      mainCubeMeshRend.material.color = colorCube.material.color;
    }
  }
}
