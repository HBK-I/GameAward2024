using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class SpiralBullet : BulletBase
{
	float m_speed;			// 弾の速度
	float m_rotTime;		// 一回転に掛かる時間
	float m_radScaleSpeed;	// 半径の拡大速度
	float m_maxSpiralRad;   // 回転の半径

	Rigidbody m_rigidbody;
	float m_radScale = 0.0f;
	float m_moveTimer = 0.0f;
	float m_rotTimer = 0.0f;
	Vector3 m_startPos;
	Vector3 m_vToTarget;

	protected override void Start()
	{
		m_bulletKind = BulletDataList.E_BULLET_KIND.SPIRAL;

		base.Start();

		TryGetComponent(out m_rigidbody);
		m_startPos = transform.position;

		MoveSpiral();	// 始めに位置を計算
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		MoveSpiral();
	}

	protected override void OnChangeTarget(E_TARGET_KIND targetKind)
	{
		//--- ターゲットへのベクトルを計算
		m_vToTarget = m_target.position - transform.position;
		m_vToTarget.Normalize();

		m_startPos = transform.position;

		m_moveTimer = 0.0f;
	}

	protected override void SetData(Dictionary<string, CSVParamData> data)
	{
		base.SetData(data);
		
		//--- 値の吸出し
		data[nameof(m_speed		)].TryGetData(out m_speed);
		data[nameof(m_rotTime	)].TryGetData(out m_rotTime);
		data[nameof(m_radScaleSpeed	)].TryGetData(out m_radScaleSpeed);
		data[nameof(m_maxSpiralRad	)].TryGetData(out m_maxSpiralRad);
	}

	void MoveSpiral()
	{
		//--- 螺旋の動きを計算
		float rot = (m_rotTimer / m_rotTime) * Mathf.PI * 2.0f;
		float offsetX = Mathf.Cos(rot) * m_radScale;
		float offsetY = Mathf.Sin(rot) * m_radScale;
		Vector3 spiralMove = transform.right * offsetX + transform.up * offsetY;

		//--- 弾の新しい位置を計算
		Vector3 forwardMove = m_vToTarget * m_speed * m_moveTimer;
		Vector3 pos = m_startPos + forwardMove + spiralMove;

		m_rigidbody.MovePosition(pos);

		//--- 回転の半径を拡大
		m_radScale += m_radScaleSpeed * Time.fixedDeltaTime;
		m_radScale = Mathf.Clamp(m_radScale, 0.0f, m_maxSpiralRad);

		// 経過時間を加算
		m_rotTimer	+= Time.fixedDeltaTime;
		m_moveTimer += Time.fixedDeltaTime;
	}
}