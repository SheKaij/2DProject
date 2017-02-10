using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Collections.ObjectModel;

public class BulletManager : GameObject
{
    private const int BULLETS_LEFT = 5;

    private List<Bullet> _listOfBullets = new List<Bullet>();

    private Bullet _currentBullet;
    private Bullet _regularBullet;
    private Bullet _slowBullet;
    private Bullet _fastBullet;

    public BulletManager()
    {
        // empty
    }
    

    private void Update()
    {
        // empty
    }

    public Bullet GetBullet()
    {
        return _currentBullet;
    }

    public List<Bullet> GetAllBullets()
    {
        return _listOfBullets;
    }
}
