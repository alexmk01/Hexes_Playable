var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i466 = root || request.c( 'UnityEngine.JointSpring' )
  var i467 = data
  i466.spring = i467[0]
  i466.damper = i467[1]
  i466.targetPosition = i467[2]
  return i466
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i468 = root || request.c( 'UnityEngine.JointMotor' )
  var i469 = data
  i468.m_TargetVelocity = i469[0]
  i468.m_Force = i469[1]
  i468.m_FreeSpin = i469[2]
  return i468
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i470 = root || request.c( 'UnityEngine.JointLimits' )
  var i471 = data
  i470.m_Min = i471[0]
  i470.m_Max = i471[1]
  i470.m_Bounciness = i471[2]
  i470.m_BounceMinVelocity = i471[3]
  i470.m_ContactDistance = i471[4]
  i470.minBounce = i471[5]
  i470.maxBounce = i471[6]
  return i470
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i472 = root || request.c( 'UnityEngine.JointDrive' )
  var i473 = data
  i472.m_PositionSpring = i473[0]
  i472.m_PositionDamper = i473[1]
  i472.m_MaximumForce = i473[2]
  i472.m_UseAcceleration = i473[3]
  return i472
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i474 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i475 = data
  i474.m_Spring = i475[0]
  i474.m_Damper = i475[1]
  return i474
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i476 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i477 = data
  i476.m_Limit = i477[0]
  i476.m_Bounciness = i477[1]
  i476.m_ContactDistance = i477[2]
  return i476
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i478 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i479 = data
  i478.m_ExtremumSlip = i479[0]
  i478.m_ExtremumValue = i479[1]
  i478.m_AsymptoteSlip = i479[2]
  i478.m_AsymptoteValue = i479[3]
  i478.m_Stiffness = i479[4]
  return i478
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i480 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i481 = data
  i480.m_LowerAngle = i481[0]
  i480.m_UpperAngle = i481[1]
  return i480
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i482 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i483 = data
  i482.m_MotorSpeed = i483[0]
  i482.m_MaximumMotorTorque = i483[1]
  return i482
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i484 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i485 = data
  i484.m_DampingRatio = i485[0]
  i484.m_Frequency = i485[1]
  i484.m_Angle = i485[2]
  return i484
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i486 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i487 = data
  i486.m_LowerTranslation = i487[0]
  i486.m_UpperTranslation = i487[1]
  return i486
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Transform"] = function (request, data, root) {
  var i488 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Transform' )
  var i489 = data
  i488.position = new pc.Vec3( i489[0], i489[1], i489[2] )
  i488.scale = new pc.Vec3( i489[3], i489[4], i489[5] )
  i488.rotation = new pc.Quat(i489[6], i489[7], i489[8], i489[9])
  return i488
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshFilter"] = function (request, data, root) {
  var i490 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshFilter' )
  var i491 = data
  request.r(i491[0], i491[1], 0, i490, 'sharedMesh')
  return i490
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshRenderer"] = function (request, data, root) {
  var i492 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshRenderer' )
  var i493 = data
  request.r(i493[0], i493[1], 0, i492, 'additionalVertexStreams')
  i492.enabled = !!i493[2]
  request.r(i493[3], i493[4], 0, i492, 'sharedMaterial')
  var i495 = i493[5]
  var i494 = []
  for(var i = 0; i < i495.length; i += 2) {
  request.r(i495[i + 0], i495[i + 1], 2, i494, '')
  }
  i492.sharedMaterials = i494
  i492.receiveShadows = !!i493[6]
  i492.shadowCastingMode = i493[7]
  i492.sortingLayerID = i493[8]
  i492.sortingOrder = i493[9]
  i492.lightmapIndex = i493[10]
  i492.lightmapSceneIndex = i493[11]
  i492.lightmapScaleOffset = new pc.Vec4( i493[12], i493[13], i493[14], i493[15] )
  i492.lightProbeUsage = i493[16]
  i492.reflectionProbeUsage = i493[17]
  return i492
}

Deserializers["Game.Entities.HexComponent"] = function (request, data, root) {
  var i498 = root || request.c( 'Game.Entities.HexComponent' )
  var i499 = data
  i498.boundsCenterOffset = new pc.Vec3( i499[0], i499[1], i499[2] )
  i498.boundsSize = new pc.Vec3( i499[3], i499[4], i499[5] )
  return i498
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i500 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i501 = data
  i500.name = i501[0]
  i500.tagId = i501[1]
  i500.enabled = !!i501[2]
  i500.isStatic = !!i501[3]
  i500.layer = i501[4]
  return i500
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh"] = function (request, data, root) {
  var i502 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh' )
  var i503 = data
  i502.name = i503[0]
  i502.halfPrecision = !!i503[1]
  i502.useSimplification = !!i503[2]
  i502.useUInt32IndexFormat = !!i503[3]
  i502.vertexCount = i503[4]
  i502.aabb = i503[5]
  var i505 = i503[6]
  var i504 = []
  for(var i = 0; i < i505.length; i += 1) {
    i504.push( !!i505[i + 0] );
  }
  i502.streams = i504
  i502.vertices = i503[7]
  var i507 = i503[8]
  var i506 = []
  for(var i = 0; i < i507.length; i += 1) {
    i506.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh', i507[i + 0]) );
  }
  i502.subMeshes = i506
  var i509 = i503[9]
  var i508 = []
  for(var i = 0; i < i509.length; i += 16) {
    i508.push( new pc.Mat4().setData(i509[i + 0], i509[i + 1], i509[i + 2], i509[i + 3],  i509[i + 4], i509[i + 5], i509[i + 6], i509[i + 7],  i509[i + 8], i509[i + 9], i509[i + 10], i509[i + 11],  i509[i + 12], i509[i + 13], i509[i + 14], i509[i + 15]) );
  }
  i502.bindposes = i508
  var i511 = i503[10]
  var i510 = []
  for(var i = 0; i < i511.length; i += 1) {
    i510.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape', i511[i + 0]) );
  }
  i502.blendShapes = i510
  return i502
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh"] = function (request, data, root) {
  var i516 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh' )
  var i517 = data
  i516.triangles = i517[0]
  return i516
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape"] = function (request, data, root) {
  var i522 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape' )
  var i523 = data
  i522.name = i523[0]
  var i525 = i523[1]
  var i524 = []
  for(var i = 0; i < i525.length; i += 1) {
    i524.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame', i525[i + 0]) );
  }
  i522.frames = i524
  return i522
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i526 = root || new pc.UnityMaterial()
  var i527 = data
  i526.name = i527[0]
  request.r(i527[1], i527[2], 0, i526, 'shader')
  i526.renderQueue = i527[3]
  i526.enableInstancing = !!i527[4]
  var i529 = i527[5]
  var i528 = []
  for(var i = 0; i < i529.length; i += 1) {
    i528.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i529[i + 0]) );
  }
  i526.floatParameters = i528
  var i531 = i527[6]
  var i530 = []
  for(var i = 0; i < i531.length; i += 1) {
    i530.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i531[i + 0]) );
  }
  i526.colorParameters = i530
  var i533 = i527[7]
  var i532 = []
  for(var i = 0; i < i533.length; i += 1) {
    i532.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i533[i + 0]) );
  }
  i526.vectorParameters = i532
  var i535 = i527[8]
  var i534 = []
  for(var i = 0; i < i535.length; i += 1) {
    i534.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i535[i + 0]) );
  }
  i526.textureParameters = i534
  var i537 = i527[9]
  var i536 = []
  for(var i = 0; i < i537.length; i += 1) {
    i536.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i537[i + 0]) );
  }
  i526.materialFlags = i536
  return i526
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i540 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i541 = data
  i540.name = i541[0]
  i540.value = i541[1]
  return i540
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i544 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i545 = data
  i544.name = i545[0]
  i544.value = new pc.Color(i545[1], i545[2], i545[3], i545[4])
  return i544
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i548 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i549 = data
  i548.name = i549[0]
  i548.value = new pc.Vec4( i549[1], i549[2], i549[3], i549[4] )
  return i548
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i552 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i553 = data
  i552.name = i553[0]
  request.r(i553[1], i553[2], 0, i552, 'value')
  return i552
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i556 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i557 = data
  i556.name = i557[0]
  i556.enabled = !!i557[1]
  return i556
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i558 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i559 = data
  i558.name = i559[0]
  i558.width = i559[1]
  i558.height = i559[2]
  i558.mipmapCount = i559[3]
  i558.anisoLevel = i559[4]
  i558.filterMode = i559[5]
  i558.hdr = !!i559[6]
  i558.format = i559[7]
  i558.wrapMode = i559[8]
  i558.alphaIsTransparency = !!i559[9]
  i558.alphaSource = i559[10]
  i558.graphicsFormat = i559[11]
  i558.sRGBTexture = !!i559[12]
  i558.desiredColorSpace = i559[13]
  i558.wrapU = i559[14]
  i558.wrapV = i559[15]
  return i558
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystem"] = function (request, data, root) {
  var i560 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystem' )
  var i561 = data
  i560.main = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule', i561[0], i560.main)
  i560.colorBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule', i561[1], i560.colorBySpeed)
  i560.colorOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule', i561[2], i560.colorOverLifetime)
  i560.emission = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule', i561[3], i560.emission)
  i560.rotationBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule', i561[4], i560.rotationBySpeed)
  i560.rotationOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule', i561[5], i560.rotationOverLifetime)
  i560.shape = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule', i561[6], i560.shape)
  i560.sizeBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule', i561[7], i560.sizeBySpeed)
  i560.sizeOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule', i561[8], i560.sizeOverLifetime)
  i560.textureSheetAnimation = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule', i561[9], i560.textureSheetAnimation)
  i560.velocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule', i561[10], i560.velocityOverLifetime)
  i560.noise = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule', i561[11], i560.noise)
  i560.inheritVelocity = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule', i561[12], i560.inheritVelocity)
  i560.forceOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule', i561[13], i560.forceOverLifetime)
  i560.limitVelocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule', i561[14], i560.limitVelocityOverLifetime)
  i560.useAutoRandomSeed = !!i561[15]
  i560.randomSeed = i561[16]
  return i560
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule"] = function (request, data, root) {
  var i562 = root || new pc.ParticleSystemMain()
  var i563 = data
  i562.duration = i563[0]
  i562.loop = !!i563[1]
  i562.prewarm = !!i563[2]
  i562.startDelay = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[3], i562.startDelay)
  i562.startLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[4], i562.startLifetime)
  i562.startSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[5], i562.startSpeed)
  i562.startSize3D = !!i563[6]
  i562.startSizeX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[7], i562.startSizeX)
  i562.startSizeY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[8], i562.startSizeY)
  i562.startSizeZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[9], i562.startSizeZ)
  i562.startRotation3D = !!i563[10]
  i562.startRotationX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[11], i562.startRotationX)
  i562.startRotationY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[12], i562.startRotationY)
  i562.startRotationZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[13], i562.startRotationZ)
  i562.startColor = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i563[14], i562.startColor)
  i562.gravityModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i563[15], i562.gravityModifier)
  i562.simulationSpace = i563[16]
  request.r(i563[17], i563[18], 0, i562, 'customSimulationSpace')
  i562.simulationSpeed = i563[19]
  i562.useUnscaledTime = !!i563[20]
  i562.scalingMode = i563[21]
  i562.playOnAwake = !!i563[22]
  i562.maxParticles = i563[23]
  i562.emitterVelocityMode = i563[24]
  i562.stopAction = i563[25]
  return i562
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve"] = function (request, data, root) {
  var i564 = root || new pc.MinMaxCurve()
  var i565 = data
  i564.mode = i565[0]
  i564.curveMin = new pc.AnimationCurve( { keys_flow: i565[1] } )
  i564.curveMax = new pc.AnimationCurve( { keys_flow: i565[2] } )
  i564.curveMultiplier = i565[3]
  i564.constantMin = i565[4]
  i564.constantMax = i565[5]
  return i564
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient"] = function (request, data, root) {
  var i566 = root || new pc.MinMaxGradient()
  var i567 = data
  i566.mode = i567[0]
  i566.gradientMin = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i567[1], i566.gradientMin)
  i566.gradientMax = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i567[2], i566.gradientMax)
  i566.colorMin = new pc.Color(i567[3], i567[4], i567[5], i567[6])
  i566.colorMax = new pc.Color(i567[7], i567[8], i567[9], i567[10])
  return i566
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient"] = function (request, data, root) {
  var i568 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient' )
  var i569 = data
  i568.mode = i569[0]
  var i571 = i569[1]
  var i570 = []
  for(var i = 0; i < i571.length; i += 1) {
    i570.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey', i571[i + 0]) );
  }
  i568.colorKeys = i570
  var i573 = i569[2]
  var i572 = []
  for(var i = 0; i < i573.length; i += 1) {
    i572.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey', i573[i + 0]) );
  }
  i568.alphaKeys = i572
  return i568
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule"] = function (request, data, root) {
  var i574 = root || new pc.ParticleSystemColorBySpeed()
  var i575 = data
  i574.enabled = !!i575[0]
  i574.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i575[1], i574.color)
  i574.range = new pc.Vec2( i575[2], i575[3] )
  return i574
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey"] = function (request, data, root) {
  var i578 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey' )
  var i579 = data
  i578.color = new pc.Color(i579[0], i579[1], i579[2], i579[3])
  i578.time = i579[4]
  return i578
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey"] = function (request, data, root) {
  var i582 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey' )
  var i583 = data
  i582.alpha = i583[0]
  i582.time = i583[1]
  return i582
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule"] = function (request, data, root) {
  var i584 = root || new pc.ParticleSystemColorOverLifetime()
  var i585 = data
  i584.enabled = !!i585[0]
  i584.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i585[1], i584.color)
  return i584
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule"] = function (request, data, root) {
  var i586 = root || new pc.ParticleSystemEmitter()
  var i587 = data
  i586.enabled = !!i587[0]
  i586.rateOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i587[1], i586.rateOverTime)
  i586.rateOverDistance = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i587[2], i586.rateOverDistance)
  var i589 = i587[3]
  var i588 = []
  for(var i = 0; i < i589.length; i += 1) {
    i588.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst', i589[i + 0]) );
  }
  i586.bursts = i588
  return i586
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst"] = function (request, data, root) {
  var i592 = root || new pc.ParticleSystemBurst()
  var i593 = data
  i592.count = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i593[0], i592.count)
  i592.cycleCount = i593[1]
  i592.minCount = i593[2]
  i592.maxCount = i593[3]
  i592.repeatInterval = i593[4]
  i592.time = i593[5]
  return i592
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule"] = function (request, data, root) {
  var i594 = root || new pc.ParticleSystemRotationBySpeed()
  var i595 = data
  i594.enabled = !!i595[0]
  i594.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i595[1], i594.x)
  i594.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i595[2], i594.y)
  i594.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i595[3], i594.z)
  i594.separateAxes = !!i595[4]
  i594.range = new pc.Vec2( i595[5], i595[6] )
  return i594
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule"] = function (request, data, root) {
  var i596 = root || new pc.ParticleSystemRotationOverLifetime()
  var i597 = data
  i596.enabled = !!i597[0]
  i596.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i597[1], i596.x)
  i596.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i597[2], i596.y)
  i596.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i597[3], i596.z)
  i596.separateAxes = !!i597[4]
  return i596
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule"] = function (request, data, root) {
  var i598 = root || new pc.ParticleSystemShape()
  var i599 = data
  i598.enabled = !!i599[0]
  i598.shapeType = i599[1]
  i598.randomDirectionAmount = i599[2]
  i598.sphericalDirectionAmount = i599[3]
  i598.randomPositionAmount = i599[4]
  i598.alignToDirection = !!i599[5]
  i598.radius = i599[6]
  i598.radiusMode = i599[7]
  i598.radiusSpread = i599[8]
  i598.radiusSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i599[9], i598.radiusSpeed)
  i598.radiusThickness = i599[10]
  i598.angle = i599[11]
  i598.length = i599[12]
  i598.boxThickness = new pc.Vec3( i599[13], i599[14], i599[15] )
  i598.meshShapeType = i599[16]
  request.r(i599[17], i599[18], 0, i598, 'mesh')
  request.r(i599[19], i599[20], 0, i598, 'meshRenderer')
  request.r(i599[21], i599[22], 0, i598, 'skinnedMeshRenderer')
  i598.useMeshMaterialIndex = !!i599[23]
  i598.meshMaterialIndex = i599[24]
  i598.useMeshColors = !!i599[25]
  i598.normalOffset = i599[26]
  i598.arc = i599[27]
  i598.arcMode = i599[28]
  i598.arcSpread = i599[29]
  i598.arcSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i599[30], i598.arcSpeed)
  i598.donutRadius = i599[31]
  i598.position = new pc.Vec3( i599[32], i599[33], i599[34] )
  i598.rotation = new pc.Vec3( i599[35], i599[36], i599[37] )
  i598.scale = new pc.Vec3( i599[38], i599[39], i599[40] )
  return i598
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule"] = function (request, data, root) {
  var i600 = root || new pc.ParticleSystemSizeBySpeed()
  var i601 = data
  i600.enabled = !!i601[0]
  i600.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i601[1], i600.x)
  i600.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i601[2], i600.y)
  i600.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i601[3], i600.z)
  i600.separateAxes = !!i601[4]
  i600.range = new pc.Vec2( i601[5], i601[6] )
  return i600
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule"] = function (request, data, root) {
  var i602 = root || new pc.ParticleSystemSizeOverLifetime()
  var i603 = data
  i602.enabled = !!i603[0]
  i602.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i603[1], i602.x)
  i602.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i603[2], i602.y)
  i602.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i603[3], i602.z)
  i602.separateAxes = !!i603[4]
  return i602
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule"] = function (request, data, root) {
  var i604 = root || new pc.ParticleSystemTextureSheetAnimation()
  var i605 = data
  i604.enabled = !!i605[0]
  i604.mode = i605[1]
  i604.animation = i605[2]
  i604.numTilesX = i605[3]
  i604.numTilesY = i605[4]
  i604.useRandomRow = !!i605[5]
  i604.frameOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[6], i604.frameOverTime)
  i604.startFrame = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i605[7], i604.startFrame)
  i604.cycleCount = i605[8]
  i604.rowIndex = i605[9]
  i604.flipU = i605[10]
  i604.flipV = i605[11]
  i604.spriteCount = i605[12]
  var i607 = i605[13]
  var i606 = []
  for(var i = 0; i < i607.length; i += 2) {
  request.r(i607[i + 0], i607[i + 1], 2, i606, '')
  }
  i604.sprites = i606
  return i604
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule"] = function (request, data, root) {
  var i610 = root || new pc.ParticleSystemVelocityOverLifetime()
  var i611 = data
  i610.enabled = !!i611[0]
  i610.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[1], i610.x)
  i610.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[2], i610.y)
  i610.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[3], i610.z)
  i610.radial = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[4], i610.radial)
  i610.speedModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[5], i610.speedModifier)
  i610.space = i611[6]
  i610.orbitalX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[7], i610.orbitalX)
  i610.orbitalY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[8], i610.orbitalY)
  i610.orbitalZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[9], i610.orbitalZ)
  i610.orbitalOffsetX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[10], i610.orbitalOffsetX)
  i610.orbitalOffsetY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[11], i610.orbitalOffsetY)
  i610.orbitalOffsetZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i611[12], i610.orbitalOffsetZ)
  return i610
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule"] = function (request, data, root) {
  var i612 = root || new pc.ParticleSystemNoise()
  var i613 = data
  i612.enabled = !!i613[0]
  i612.separateAxes = !!i613[1]
  i612.strengthX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[2], i612.strengthX)
  i612.strengthY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[3], i612.strengthY)
  i612.strengthZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[4], i612.strengthZ)
  i612.frequency = i613[5]
  i612.damping = !!i613[6]
  i612.octaveCount = i613[7]
  i612.octaveMultiplier = i613[8]
  i612.octaveScale = i613[9]
  i612.quality = i613[10]
  i612.scrollSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[11], i612.scrollSpeed)
  i612.scrollSpeedMultiplier = i613[12]
  i612.remapEnabled = !!i613[13]
  i612.remapX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[14], i612.remapX)
  i612.remapY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[15], i612.remapY)
  i612.remapZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[16], i612.remapZ)
  i612.positionAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[17], i612.positionAmount)
  i612.rotationAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[18], i612.rotationAmount)
  i612.sizeAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i613[19], i612.sizeAmount)
  return i612
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule"] = function (request, data, root) {
  var i614 = root || new pc.ParticleSystemInheritVelocity()
  var i615 = data
  i614.enabled = !!i615[0]
  i614.mode = i615[1]
  i614.curve = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i615[2], i614.curve)
  return i614
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule"] = function (request, data, root) {
  var i616 = root || new pc.ParticleSystemForceOverLifetime()
  var i617 = data
  i616.enabled = !!i617[0]
  i616.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i617[1], i616.x)
  i616.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i617[2], i616.y)
  i616.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i617[3], i616.z)
  i616.space = i617[4]
  i616.randomized = !!i617[5]
  return i616
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule"] = function (request, data, root) {
  var i618 = root || new pc.ParticleSystemLimitVelocityOverLifetime()
  var i619 = data
  i618.enabled = !!i619[0]
  i618.limit = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i619[1], i618.limit)
  i618.limitX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i619[2], i618.limitX)
  i618.limitY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i619[3], i618.limitY)
  i618.limitZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i619[4], i618.limitZ)
  i618.dampen = i619[5]
  i618.separateAxes = !!i619[6]
  i618.space = i619[7]
  i618.drag = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i619[8], i618.drag)
  i618.multiplyDragByParticleSize = !!i619[9]
  i618.multiplyDragByParticleVelocity = !!i619[10]
  return i618
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer"] = function (request, data, root) {
  var i620 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer' )
  var i621 = data
  request.r(i621[0], i621[1], 0, i620, 'mesh')
  i620.meshCount = i621[2]
  i620.activeVertexStreamsCount = i621[3]
  i620.alignment = i621[4]
  i620.renderMode = i621[5]
  i620.sortMode = i621[6]
  i620.lengthScale = i621[7]
  i620.velocityScale = i621[8]
  i620.cameraVelocityScale = i621[9]
  i620.normalDirection = i621[10]
  i620.sortingFudge = i621[11]
  i620.minParticleSize = i621[12]
  i620.maxParticleSize = i621[13]
  i620.pivot = new pc.Vec3( i621[14], i621[15], i621[16] )
  request.r(i621[17], i621[18], 0, i620, 'trailMaterial')
  i620.applyActiveColorSpace = !!i621[19]
  i620.enabled = !!i621[20]
  request.r(i621[21], i621[22], 0, i620, 'sharedMaterial')
  var i623 = i621[23]
  var i622 = []
  for(var i = 0; i < i623.length; i += 2) {
  request.r(i623[i + 0], i623[i + 1], 2, i622, '')
  }
  i620.sharedMaterials = i622
  i620.receiveShadows = !!i621[24]
  i620.shadowCastingMode = i621[25]
  i620.sortingLayerID = i621[26]
  i620.sortingOrder = i621[27]
  i620.lightmapIndex = i621[28]
  i620.lightmapSceneIndex = i621[29]
  i620.lightmapScaleOffset = new pc.Vec4( i621[30], i621[31], i621[32], i621[33] )
  i620.lightProbeUsage = i621[34]
  i620.reflectionProbeUsage = i621[35]
  return i620
}

Deserializers["Game.World.HexGridComponent"] = function (request, data, root) {
  var i624 = root || request.c( 'Game.World.HexGridComponent' )
  var i625 = data
  i624.size = i625[0]
  i624.gridRadius = i625[1]
  i624.drawCells = !!i625[2]
  i624.drawCenters = !!i625[3]
  i624.drawCoordinates = !!i625[4]
  i624.cellColor = new pc.Color(i625[5], i625[6], i625[7], i625[8])
  i624.centerColor = new pc.Color(i625[9], i625[10], i625[11], i625[12])
  i624.busyCenterColor = new pc.Color(i625[13], i625[14], i625[15], i625[16])
  return i624
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.RectTransform"] = function (request, data, root) {
  var i626 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.RectTransform' )
  var i627 = data
  i626.pivot = new pc.Vec2( i627[0], i627[1] )
  i626.anchorMin = new pc.Vec2( i627[2], i627[3] )
  i626.anchorMax = new pc.Vec2( i627[4], i627[5] )
  i626.sizeDelta = new pc.Vec2( i627[6], i627[7] )
  i626.anchoredPosition3D = new pc.Vec3( i627[8], i627[9], i627[10] )
  i626.rotation = new pc.Quat(i627[11], i627[12], i627[13], i627[14])
  i626.scale = new pc.Vec3( i627[15], i627[16], i627[17] )
  return i626
}

Deserializers["Game.UI.TutorialScreenViewComponent"] = function (request, data, root) {
  var i628 = root || request.c( 'Game.UI.TutorialScreenViewComponent' )
  var i629 = data
  i628.PointerAnimationDuration = i629[0]
  request.r(i629[1], i629[2], 0, i628, 'hexPointerImage')
  return i628
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer"] = function (request, data, root) {
  var i630 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer' )
  var i631 = data
  i630.cullTransparentMesh = !!i631[0]
  return i630
}

Deserializers["UnityEngine.UI.Image"] = function (request, data, root) {
  var i632 = root || request.c( 'UnityEngine.UI.Image' )
  var i633 = data
  request.r(i633[0], i633[1], 0, i632, 'm_Sprite')
  i632.m_Type = i633[2]
  i632.m_PreserveAspect = !!i633[3]
  i632.m_FillCenter = !!i633[4]
  i632.m_FillMethod = i633[5]
  i632.m_FillAmount = i633[6]
  i632.m_FillClockwise = !!i633[7]
  i632.m_FillOrigin = i633[8]
  i632.m_UseSpriteMesh = !!i633[9]
  i632.m_PixelsPerUnitMultiplier = i633[10]
  request.r(i633[11], i633[12], 0, i632, 'm_Material')
  i632.m_Maskable = !!i633[13]
  i632.m_Color = new pc.Color(i633[14], i633[15], i633[16], i633[17])
  i632.m_RaycastTarget = !!i633[18]
  i632.m_RaycastPadding = new pc.Vec4( i633[19], i633[20], i633[21], i633[22] )
  return i632
}

Deserializers["Game.UI.GameEndScreenViewComponent"] = function (request, data, root) {
  var i634 = root || request.c( 'Game.UI.GameEndScreenViewComponent' )
  var i635 = data
  i634.FadeInDuration = i635[0]
  request.r(i635[1], i635[2], 0, i634, 'exitButton')
  return i634
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasGroup"] = function (request, data, root) {
  var i636 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasGroup' )
  var i637 = data
  i636.m_Alpha = i637[0]
  i636.m_Interactable = !!i637[1]
  i636.m_BlocksRaycasts = !!i637[2]
  i636.m_IgnoreParentGroups = !!i637[3]
  i636.enabled = !!i637[4]
  return i636
}

Deserializers["UnityEngine.UI.Button"] = function (request, data, root) {
  var i638 = root || request.c( 'UnityEngine.UI.Button' )
  var i639 = data
  i638.m_OnClick = request.d('UnityEngine.UI.Button+ButtonClickedEvent', i639[0], i638.m_OnClick)
  i638.m_Navigation = request.d('UnityEngine.UI.Navigation', i639[1], i638.m_Navigation)
  i638.m_Transition = i639[2]
  i638.m_Colors = request.d('UnityEngine.UI.ColorBlock', i639[3], i638.m_Colors)
  i638.m_SpriteState = request.d('UnityEngine.UI.SpriteState', i639[4], i638.m_SpriteState)
  i638.m_AnimationTriggers = request.d('UnityEngine.UI.AnimationTriggers', i639[5], i638.m_AnimationTriggers)
  i638.m_Interactable = !!i639[6]
  request.r(i639[7], i639[8], 0, i638, 'm_TargetGraphic')
  return i638
}

Deserializers["UnityEngine.UI.Button+ButtonClickedEvent"] = function (request, data, root) {
  var i640 = root || request.c( 'UnityEngine.UI.Button+ButtonClickedEvent' )
  var i641 = data
  i640.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i641[0], i640.m_PersistentCalls)
  return i640
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i642 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i643 = data
  var i645 = i643[0]
  var i644 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i645.length; i += 1) {
    i644.add(request.d('UnityEngine.Events.PersistentCall', i645[i + 0]));
  }
  i642.m_Calls = i644
  return i642
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i648 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i649 = data
  request.r(i649[0], i649[1], 0, i648, 'm_Target')
  i648.m_TargetAssemblyTypeName = i649[2]
  i648.m_MethodName = i649[3]
  i648.m_Mode = i649[4]
  i648.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i649[5], i648.m_Arguments)
  i648.m_CallState = i649[6]
  return i648
}

Deserializers["UnityEngine.UI.Navigation"] = function (request, data, root) {
  var i650 = root || request.c( 'UnityEngine.UI.Navigation' )
  var i651 = data
  i650.m_Mode = i651[0]
  i650.m_WrapAround = !!i651[1]
  request.r(i651[2], i651[3], 0, i650, 'm_SelectOnUp')
  request.r(i651[4], i651[5], 0, i650, 'm_SelectOnDown')
  request.r(i651[6], i651[7], 0, i650, 'm_SelectOnLeft')
  request.r(i651[8], i651[9], 0, i650, 'm_SelectOnRight')
  return i650
}

Deserializers["UnityEngine.UI.ColorBlock"] = function (request, data, root) {
  var i652 = root || request.c( 'UnityEngine.UI.ColorBlock' )
  var i653 = data
  i652.m_NormalColor = new pc.Color(i653[0], i653[1], i653[2], i653[3])
  i652.m_HighlightedColor = new pc.Color(i653[4], i653[5], i653[6], i653[7])
  i652.m_PressedColor = new pc.Color(i653[8], i653[9], i653[10], i653[11])
  i652.m_SelectedColor = new pc.Color(i653[12], i653[13], i653[14], i653[15])
  i652.m_DisabledColor = new pc.Color(i653[16], i653[17], i653[18], i653[19])
  i652.m_ColorMultiplier = i653[20]
  i652.m_FadeDuration = i653[21]
  return i652
}

Deserializers["UnityEngine.UI.SpriteState"] = function (request, data, root) {
  var i654 = root || request.c( 'UnityEngine.UI.SpriteState' )
  var i655 = data
  request.r(i655[0], i655[1], 0, i654, 'm_HighlightedSprite')
  request.r(i655[2], i655[3], 0, i654, 'm_PressedSprite')
  request.r(i655[4], i655[5], 0, i654, 'm_SelectedSprite')
  request.r(i655[6], i655[7], 0, i654, 'm_DisabledSprite')
  return i654
}

Deserializers["UnityEngine.UI.AnimationTriggers"] = function (request, data, root) {
  var i656 = root || request.c( 'UnityEngine.UI.AnimationTriggers' )
  var i657 = data
  i656.m_NormalTrigger = i657[0]
  i656.m_HighlightedTrigger = i657[1]
  i656.m_PressedTrigger = i657[2]
  i656.m_SelectedTrigger = i657[3]
  i656.m_DisabledTrigger = i657[4]
  return i656
}

Deserializers["UnityEngine.UI.Outline"] = function (request, data, root) {
  var i658 = root || request.c( 'UnityEngine.UI.Outline' )
  var i659 = data
  i658.m_EffectColor = new pc.Color(i659[0], i659[1], i659[2], i659[3])
  i658.m_EffectDistance = new pc.Vec2( i659[4], i659[5] )
  i658.m_UseGraphicAlpha = !!i659[6]
  return i658
}

Deserializers["UnityEngine.UI.Text"] = function (request, data, root) {
  var i660 = root || request.c( 'UnityEngine.UI.Text' )
  var i661 = data
  i660.m_FontData = request.d('UnityEngine.UI.FontData', i661[0], i660.m_FontData)
  i660.m_Text = i661[1]
  request.r(i661[2], i661[3], 0, i660, 'm_Material')
  i660.m_Maskable = !!i661[4]
  i660.m_Color = new pc.Color(i661[5], i661[6], i661[7], i661[8])
  i660.m_RaycastTarget = !!i661[9]
  i660.m_RaycastPadding = new pc.Vec4( i661[10], i661[11], i661[12], i661[13] )
  return i660
}

Deserializers["UnityEngine.UI.FontData"] = function (request, data, root) {
  var i662 = root || request.c( 'UnityEngine.UI.FontData' )
  var i663 = data
  request.r(i663[0], i663[1], 0, i662, 'm_Font')
  i662.m_FontSize = i663[2]
  i662.m_FontStyle = i663[3]
  i662.m_BestFit = !!i663[4]
  i662.m_MinSize = i663[5]
  i662.m_MaxSize = i663[6]
  i662.m_Alignment = i663[7]
  i662.m_AlignByGeometry = !!i663[8]
  i662.m_RichText = !!i663[9]
  i662.m_HorizontalOverflow = i663[10]
  i662.m_VerticalOverflow = i663[11]
  i662.m_LineSpacing = i663[12]
  return i662
}

Deserializers["UnityEngine.UI.Shadow"] = function (request, data, root) {
  var i664 = root || request.c( 'UnityEngine.UI.Shadow' )
  var i665 = data
  i664.m_EffectColor = new pc.Color(i665[0], i665[1], i665[2], i665[3])
  i664.m_EffectDistance = new pc.Vec2( i665[4], i665[5] )
  i664.m_UseGraphicAlpha = !!i665[6]
  return i664
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i666 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i667 = data
  i666.name = i667[0]
  i666.index = i667[1]
  i666.startup = !!i667[2]
  return i666
}

Deserializers["Zenject.SceneContext"] = function (request, data, root) {
  var i668 = root || request.c( 'Zenject.SceneContext' )
  var i669 = data
  i668.OnPreInstall = request.d('UnityEngine.Events.UnityEvent', i669[0], i668.OnPreInstall)
  i668.OnPostInstall = request.d('UnityEngine.Events.UnityEvent', i669[1], i668.OnPostInstall)
  i668.OnPreResolve = request.d('UnityEngine.Events.UnityEvent', i669[2], i668.OnPreResolve)
  i668.OnPostResolve = request.d('UnityEngine.Events.UnityEvent', i669[3], i668.OnPostResolve)
  i668._parentNewObjectsUnderSceneContext = !!i669[4]
  var i671 = i669[5]
  var i670 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i671.length; i += 1) {
    i670.add(i671[i + 0]);
  }
  i668._contractNames = i670
  var i673 = i669[6]
  var i672 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i673.length; i += 1) {
    i672.add(i673[i + 0]);
  }
  i668._parentContractNames = i672
  i668._autoRun = !!i669[7]
  var i675 = i669[8]
  var i674 = new (System.Collections.Generic.List$1(Bridge.ns('Zenject.ScriptableObjectInstaller')))
  for(var i = 0; i < i675.length; i += 2) {
  request.r(i675[i + 0], i675[i + 1], 1, i674, '')
  }
  i668._scriptableObjectInstallers = i674
  var i677 = i669[9]
  var i676 = new (System.Collections.Generic.List$1(Bridge.ns('Zenject.MonoInstaller')))
  for(var i = 0; i < i677.length; i += 2) {
  request.r(i677[i + 0], i677[i + 1], 1, i676, '')
  }
  i668._monoInstallers = i676
  var i679 = i669[10]
  var i678 = new (System.Collections.Generic.List$1(Bridge.ns('Zenject.MonoInstaller')))
  for(var i = 0; i < i679.length; i += 2) {
  request.r(i679[i + 0], i679[i + 1], 1, i678, '')
  }
  i668._installerPrefabs = i678
  return i668
}

Deserializers["UnityEngine.Events.UnityEvent"] = function (request, data, root) {
  var i680 = root || request.c( 'UnityEngine.Events.UnityEvent' )
  var i681 = data
  i680.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i681[0], i680.m_PersistentCalls)
  return i680
}

Deserializers["Game.Bootstrap.GameRunInstaller"] = function (request, data, root) {
  var i688 = root || request.c( 'Game.Bootstrap.GameRunInstaller' )
  var i689 = data
  request.r(i689[0], i689[1], 0, i688, 'SceneRoot')
  request.r(i689[2], i689[3], 0, i688, 'GameConfig')
  request.r(i689[4], i689[5], 0, i688, 'HexGridPrefab')
  request.r(i689[6], i689[7], 0, i688, 'PlayerHexStacksSpawnPointsRoot')
  return i688
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Canvas"] = function (request, data, root) {
  var i690 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Canvas' )
  var i691 = data
  i690.planeDistance = i691[0]
  i690.referencePixelsPerUnit = i691[1]
  i690.isFallbackOverlay = !!i691[2]
  i690.renderMode = i691[3]
  i690.renderOrder = i691[4]
  i690.sortingLayerName = i691[5]
  i690.sortingOrder = i691[6]
  i690.scaleFactor = i691[7]
  request.r(i691[8], i691[9], 0, i690, 'worldCamera')
  i690.overrideSorting = !!i691[10]
  i690.pixelPerfect = !!i691[11]
  i690.targetDisplay = i691[12]
  i690.overridePixelPerfect = !!i691[13]
  i690.enabled = !!i691[14]
  return i690
}

Deserializers["UnityEngine.UI.CanvasScaler"] = function (request, data, root) {
  var i692 = root || request.c( 'UnityEngine.UI.CanvasScaler' )
  var i693 = data
  i692.m_UiScaleMode = i693[0]
  i692.m_ReferencePixelsPerUnit = i693[1]
  i692.m_ScaleFactor = i693[2]
  i692.m_ReferenceResolution = new pc.Vec2( i693[3], i693[4] )
  i692.m_ScreenMatchMode = i693[5]
  i692.m_MatchWidthOrHeight = i693[6]
  i692.m_PhysicalUnit = i693[7]
  i692.m_FallbackScreenDPI = i693[8]
  i692.m_DefaultSpriteDPI = i693[9]
  i692.m_DynamicPixelsPerUnit = i693[10]
  i692.m_PresetInfoIsWorld = !!i693[11]
  return i692
}

Deserializers["UnityEngine.UI.GraphicRaycaster"] = function (request, data, root) {
  var i694 = root || request.c( 'UnityEngine.UI.GraphicRaycaster' )
  var i695 = data
  i694.m_IgnoreReversedGraphics = !!i695[0]
  i694.m_BlockingObjects = i695[1]
  i694.m_BlockingMask = UnityEngine.LayerMask.FromIntegerValue( i695[2] )
  return i694
}

Deserializers["Game.Bootstrap.GameUIInstaller"] = function (request, data, root) {
  var i696 = root || request.c( 'Game.Bootstrap.GameUIInstaller' )
  var i697 = data
  request.r(i697[0], i697[1], 0, i696, 'MainCanvas')
  request.r(i697[2], i697[3], 0, i696, 'TutorialScreenViewPrefab')
  request.r(i697[4], i697[5], 0, i696, 'WinScreenViewPrefab')
  request.r(i697[6], i697[7], 0, i696, 'LoseScreenViewPrefab')
  return i696
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i698 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i699 = data
  i698.aspect = i699[0]
  i698.orthographic = !!i699[1]
  i698.orthographicSize = i699[2]
  i698.backgroundColor = new pc.Color(i699[3], i699[4], i699[5], i699[6])
  i698.nearClipPlane = i699[7]
  i698.farClipPlane = i699[8]
  i698.fieldOfView = i699[9]
  i698.depth = i699[10]
  i698.clearFlags = i699[11]
  i698.cullingMask = i699[12]
  i698.rect = i699[13]
  request.r(i699[14], i699[15], 0, i698, 'targetTexture')
  i698.usePhysicalProperties = !!i699[16]
  i698.focalLength = i699[17]
  i698.sensorSize = new pc.Vec2( i699[18], i699[19] )
  i698.lensShift = new pc.Vec2( i699[20], i699[21] )
  i698.gateFit = i699[22]
  i698.commandBufferCount = i699[23]
  i698.cameraType = i699[24]
  i698.enabled = !!i699[25]
  return i698
}

Deserializers["UnityEngine.Rendering.Universal.UniversalAdditionalCameraData"] = function (request, data, root) {
  var i700 = root || request.c( 'UnityEngine.Rendering.Universal.UniversalAdditionalCameraData' )
  var i701 = data
  i700.m_RenderShadows = !!i701[0]
  i700.m_RequiresDepthTextureOption = i701[1]
  i700.m_RequiresOpaqueTextureOption = i701[2]
  i700.m_CameraType = i701[3]
  var i703 = i701[4]
  var i702 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Camera')))
  for(var i = 0; i < i703.length; i += 2) {
  request.r(i703[i + 0], i703[i + 1], 1, i702, '')
  }
  i700.m_Cameras = i702
  i700.m_RendererIndex = i701[5]
  i700.m_VolumeLayerMask = UnityEngine.LayerMask.FromIntegerValue( i701[6] )
  request.r(i701[7], i701[8], 0, i700, 'm_VolumeTrigger')
  i700.m_VolumeFrameworkUpdateModeOption = i701[9]
  i700.m_RenderPostProcessing = !!i701[10]
  i700.m_Antialiasing = i701[11]
  i700.m_AntialiasingQuality = i701[12]
  i700.m_StopNaN = !!i701[13]
  i700.m_Dithering = !!i701[14]
  i700.m_ClearDepth = !!i701[15]
  i700.m_AllowXRRendering = !!i701[16]
  i700.m_AllowHDROutput = !!i701[17]
  i700.m_UseScreenCoordOverride = !!i701[18]
  i700.m_ScreenSizeOverride = new pc.Vec4( i701[19], i701[20], i701[21], i701[22] )
  i700.m_ScreenCoordScaleBias = new pc.Vec4( i701[23], i701[24], i701[25], i701[26] )
  i700.m_RequiresDepthTexture = !!i701[27]
  i700.m_RequiresColorTexture = !!i701[28]
  i700.m_Version = i701[29]
  i700.m_TaaSettings = request.d('UnityEngine.Rendering.Universal.TemporalAA+Settings', i701[30], i700.m_TaaSettings)
  return i700
}

Deserializers["UnityEngine.Rendering.Universal.TemporalAA+Settings"] = function (request, data, root) {
  var i706 = root || request.c( 'UnityEngine.Rendering.Universal.TemporalAA+Settings' )
  var i707 = data
  i706.m_Quality = i707[0]
  i706.m_FrameInfluence = i707[1]
  i706.m_JitterScale = i707[2]
  i706.m_MipBias = i707[3]
  i706.m_VarianceClampScale = i707[4]
  i706.m_ContrastAdaptiveSharpening = i707[5]
  return i706
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Light"] = function (request, data, root) {
  var i708 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Light' )
  var i709 = data
  i708.type = i709[0]
  i708.color = new pc.Color(i709[1], i709[2], i709[3], i709[4])
  i708.cullingMask = i709[5]
  i708.intensity = i709[6]
  i708.range = i709[7]
  i708.spotAngle = i709[8]
  i708.shadows = i709[9]
  i708.shadowNormalBias = i709[10]
  i708.shadowBias = i709[11]
  i708.shadowStrength = i709[12]
  i708.shadowResolution = i709[13]
  i708.lightmapBakeType = i709[14]
  i708.renderMode = i709[15]
  request.r(i709[16], i709[17], 0, i708, 'cookie')
  i708.cookieSize = i709[18]
  i708.shadowNearPlane = i709[19]
  i708.occlusionMaskChannel = i709[20]
  i708.isBaked = !!i709[21]
  i708.mixedLightingMode = i709[22]
  i708.enabled = !!i709[23]
  return i708
}

Deserializers["UnityEngine.Rendering.Universal.UniversalAdditionalLightData"] = function (request, data, root) {
  var i710 = root || request.c( 'UnityEngine.Rendering.Universal.UniversalAdditionalLightData' )
  var i711 = data
  i710.m_Version = i711[0]
  i710.m_UsePipelineSettings = !!i711[1]
  i710.m_AdditionalLightsShadowResolutionTier = i711[2]
  i710.m_LightLayerMask = i711[3]
  i710.m_RenderingLayers = i711[4]
  i710.m_CustomShadowLayers = !!i711[5]
  i710.m_ShadowLayerMask = i711[6]
  i710.m_ShadowRenderingLayers = i711[7]
  i710.m_LightCookieSize = new pc.Vec2( i711[8], i711[9] )
  i710.m_LightCookieOffset = new pc.Vec2( i711[10], i711[11] )
  i710.m_SoftShadowQuality = i711[12]
  return i710
}

Deserializers["UnityEngine.EventSystems.EventSystem"] = function (request, data, root) {
  var i712 = root || request.c( 'UnityEngine.EventSystems.EventSystem' )
  var i713 = data
  request.r(i713[0], i713[1], 0, i712, 'm_FirstSelected')
  i712.m_sendNavigationEvents = !!i713[2]
  i712.m_DragThreshold = i713[3]
  return i712
}

Deserializers["UnityEngine.EventSystems.StandaloneInputModule"] = function (request, data, root) {
  var i714 = root || request.c( 'UnityEngine.EventSystems.StandaloneInputModule' )
  var i715 = data
  i714.m_HorizontalAxis = i715[0]
  i714.m_VerticalAxis = i715[1]
  i714.m_SubmitButton = i715[2]
  i714.m_CancelButton = i715[3]
  i714.m_InputActionsPerSecond = i715[4]
  i714.m_RepeatDelay = i715[5]
  i714.m_ForceModuleActive = !!i715[6]
  i714.m_SendPointerHoverToParent = !!i715[7]
  return i714
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i716 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i717 = data
  i716.ambientIntensity = i717[0]
  i716.reflectionIntensity = i717[1]
  i716.ambientMode = i717[2]
  i716.ambientLight = new pc.Color(i717[3], i717[4], i717[5], i717[6])
  i716.ambientSkyColor = new pc.Color(i717[7], i717[8], i717[9], i717[10])
  i716.ambientGroundColor = new pc.Color(i717[11], i717[12], i717[13], i717[14])
  i716.ambientEquatorColor = new pc.Color(i717[15], i717[16], i717[17], i717[18])
  i716.fogColor = new pc.Color(i717[19], i717[20], i717[21], i717[22])
  i716.fogEndDistance = i717[23]
  i716.fogStartDistance = i717[24]
  i716.fogDensity = i717[25]
  i716.fog = !!i717[26]
  request.r(i717[27], i717[28], 0, i716, 'skybox')
  i716.fogMode = i717[29]
  var i719 = i717[30]
  var i718 = []
  for(var i = 0; i < i719.length; i += 1) {
    i718.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i719[i + 0]) );
  }
  i716.lightmaps = i718
  i716.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i717[31], i716.lightProbes)
  i716.lightmapsMode = i717[32]
  i716.mixedBakeMode = i717[33]
  i716.environmentLightingMode = i717[34]
  i716.ambientProbe = new pc.SphericalHarmonicsL2(i717[35])
  request.r(i717[36], i717[37], 0, i716, 'customReflection')
  request.r(i717[38], i717[39], 0, i716, 'defaultReflection')
  i716.defaultReflectionMode = i717[40]
  i716.defaultReflectionResolution = i717[41]
  i716.sunLightObjectId = i717[42]
  i716.pixelLightCount = i717[43]
  i716.defaultReflectionHDR = !!i717[44]
  i716.hasLightDataAsset = !!i717[45]
  i716.hasManualGenerate = !!i717[46]
  return i716
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i722 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i723 = data
  request.r(i723[0], i723[1], 0, i722, 'lightmapColor')
  request.r(i723[2], i723[3], 0, i722, 'lightmapDirection')
  request.r(i723[4], i723[5], 0, i722, 'shadowMask')
  return i722
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i724 = root || new UnityEngine.LightProbes()
  var i725 = data
  return i724
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset"] = function (request, data, root) {
  var i732 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset' )
  var i733 = data
  i732.AdditionalLightsRenderingMode = i733[0]
  i732.LightRenderingMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode', i733[1], i732.LightRenderingMode)
  i732.MainLightRenderingModeValue = i733[2]
  i732.SupportsMainLightShadows = !!i733[3]
  i732.MixedLightingSupported = !!i733[4]
  i732.MainLightShadowmapResolutionValue = i733[5]
  i732.SupportsSoftShadows = !!i733[6]
  i732.SoftShadowQualityValue = i733[7]
  i732.ShadowDistance = i733[8]
  i732.ShadowCascadeCount = i733[9]
  i732.Cascade2Split = i733[10]
  i732.Cascade3Split = new pc.Vec2( i733[11], i733[12] )
  i732.Cascade4Split = new pc.Vec3( i733[13], i733[14], i733[15] )
  i732.CascadeBorder = i733[16]
  i732.ShadowDepthBias = i733[17]
  i732.ShadowNormalBias = i733[18]
  i732.RequireDepthTexture = !!i733[19]
  i732.RequireOpaqueTexture = !!i733[20]
  i732.scriptableRendererData = request.d('Luna.Unity.DTO.UnityEngine.Assets.ScriptableRendererData', i733[21], i732.scriptableRendererData)
  return i732
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode"] = function (request, data, root) {
  var i734 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode' )
  var i735 = data
  i734.Disabled = i735[0]
  i734.PerVertex = i735[1]
  i734.PerPixel = i735[2]
  return i734
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ScriptableRendererData"] = function (request, data, root) {
  var i736 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ScriptableRendererData' )
  var i737 = data
  i736.opaqueLayerMask = i737[0]
  i736.transparentLayerMask = i737[1]
  var i739 = i737[2]
  var i738 = []
  for(var i = 0; i < i739.length; i += 1) {
    i738.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderObjects', i739[i + 0]) );
  }
  i736.RenderObjectsFeatures = i738
  i736.name = i737[3]
  return i736
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderObjects"] = function (request, data, root) {
  var i742 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderObjects' )
  var i743 = data
  i742.settings = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+RenderObjectsSettings', i743[0], i742.settings)
  i742.name = i743[1]
  i742.typeName = i743[2]
  return i742
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i744 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i745 = data
  var i747 = i745[0]
  var i746 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i747.length; i += 1) {
    i746.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i747[i + 0]));
  }
  i744.ShaderCompilationErrors = i746
  i744.name = i745[1]
  i744.guid = i745[2]
  var i749 = i745[3]
  var i748 = []
  for(var i = 0; i < i749.length; i += 1) {
    i748.push( i749[i + 0] );
  }
  i744.shaderDefinedKeywords = i748
  var i751 = i745[4]
  var i750 = []
  for(var i = 0; i < i751.length; i += 1) {
    i750.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i751[i + 0]) );
  }
  i744.passes = i750
  var i753 = i745[5]
  var i752 = []
  for(var i = 0; i < i753.length; i += 1) {
    i752.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i753[i + 0]) );
  }
  i744.usePasses = i752
  var i755 = i745[6]
  var i754 = []
  for(var i = 0; i < i755.length; i += 1) {
    i754.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i755[i + 0]) );
  }
  i744.defaultParameterValues = i754
  request.r(i745[7], i745[8], 0, i744, 'unityFallbackShader')
  i744.readDepth = !!i745[9]
  i744.hasDepthOnlyPass = !!i745[10]
  i744.isCreatedByShaderGraph = !!i745[11]
  i744.disableBatching = !!i745[12]
  i744.compiled = !!i745[13]
  return i744
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i758 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i759 = data
  i758.shaderName = i759[0]
  i758.errorMessage = i759[1]
  return i758
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i764 = root || new pc.UnityShaderPass()
  var i765 = data
  i764.id = i765[0]
  i764.subShaderIndex = i765[1]
  i764.name = i765[2]
  i764.passType = i765[3]
  i764.grabPassTextureName = i765[4]
  i764.usePass = !!i765[5]
  i764.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i765[6], i764.zTest)
  i764.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i765[7], i764.zWrite)
  i764.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i765[8], i764.culling)
  i764.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i765[9], i764.blending)
  i764.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i765[10], i764.alphaBlending)
  i764.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i765[11], i764.colorWriteMask)
  i764.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i765[12], i764.offsetUnits)
  i764.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i765[13], i764.offsetFactor)
  i764.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i765[14], i764.stencilRef)
  i764.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i765[15], i764.stencilReadMask)
  i764.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i765[16], i764.stencilWriteMask)
  i764.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i765[17], i764.stencilOp)
  i764.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i765[18], i764.stencilOpFront)
  i764.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i765[19], i764.stencilOpBack)
  var i767 = i765[20]
  var i766 = []
  for(var i = 0; i < i767.length; i += 1) {
    i766.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i767[i + 0]) );
  }
  i764.tags = i766
  var i769 = i765[21]
  var i768 = []
  for(var i = 0; i < i769.length; i += 1) {
    i768.push( i769[i + 0] );
  }
  i764.passDefinedKeywords = i768
  var i771 = i765[22]
  var i770 = []
  for(var i = 0; i < i771.length; i += 1) {
    i770.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i771[i + 0]) );
  }
  i764.passDefinedKeywordGroups = i770
  var i773 = i765[23]
  var i772 = []
  for(var i = 0; i < i773.length; i += 1) {
    i772.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i773[i + 0]) );
  }
  i764.variants = i772
  var i775 = i765[24]
  var i774 = []
  for(var i = 0; i < i775.length; i += 1) {
    i774.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i775[i + 0]) );
  }
  i764.excludedVariants = i774
  i764.hasDepthReader = !!i765[25]
  return i764
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i776 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i777 = data
  i776.val = i777[0]
  i776.name = i777[1]
  return i776
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i778 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i779 = data
  i778.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i779[0], i778.src)
  i778.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i779[1], i778.dst)
  i778.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i779[2], i778.op)
  return i778
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i780 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i781 = data
  i780.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i781[0], i780.pass)
  i780.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i781[1], i780.fail)
  i780.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i781[2], i780.zFail)
  i780.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i781[3], i780.comp)
  return i780
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i784 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i785 = data
  i784.name = i785[0]
  i784.value = i785[1]
  return i784
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i788 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i789 = data
  var i791 = i789[0]
  var i790 = []
  for(var i = 0; i < i791.length; i += 1) {
    i790.push( i791[i + 0] );
  }
  i788.keywords = i790
  i788.hasDiscard = !!i789[1]
  return i788
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i794 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i795 = data
  i794.passId = i795[0]
  i794.subShaderIndex = i795[1]
  var i797 = i795[2]
  var i796 = []
  for(var i = 0; i < i797.length; i += 1) {
    i796.push( i797[i + 0] );
  }
  i794.keywords = i796
  i794.vertexProgram = i795[3]
  i794.fragmentProgram = i795[4]
  i794.exportedForWebGl2 = !!i795[5]
  i794.readDepth = !!i795[6]
  return i794
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i800 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i801 = data
  request.r(i801[0], i801[1], 0, i800, 'shader')
  i800.pass = i801[2]
  return i800
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i804 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i805 = data
  i804.name = i805[0]
  i804.type = i805[1]
  i804.value = new pc.Vec4( i805[2], i805[3], i805[4], i805[5] )
  i804.textureValue = i805[6]
  i804.shaderPropertyFlag = i805[7]
  return i804
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Sprite"] = function (request, data, root) {
  var i806 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Sprite' )
  var i807 = data
  i806.name = i807[0]
  request.r(i807[1], i807[2], 0, i806, 'texture')
  i806.aabb = i807[3]
  i806.vertices = i807[4]
  i806.triangles = i807[5]
  i806.textureRect = UnityEngine.Rect.MinMaxRect(i807[6], i807[7], i807[8], i807[9])
  i806.packedRect = UnityEngine.Rect.MinMaxRect(i807[10], i807[11], i807[12], i807[13])
  i806.border = new pc.Vec4( i807[14], i807[15], i807[16], i807[17] )
  i806.transparency = i807[18]
  i806.bounds = i807[19]
  i806.pixelsPerUnit = i807[20]
  i806.textureWidth = i807[21]
  i806.textureHeight = i807[22]
  i806.nativeSize = new pc.Vec2( i807[23], i807[24] )
  i806.pivot = new pc.Vec2( i807[25], i807[26] )
  i806.textureRectOffset = new pc.Vec2( i807[27], i807[28] )
  return i806
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font"] = function (request, data, root) {
  var i808 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font' )
  var i809 = data
  i808.name = i809[0]
  i808.ascent = i809[1]
  i808.originalLineHeight = i809[2]
  i808.fontSize = i809[3]
  var i811 = i809[4]
  var i810 = []
  for(var i = 0; i < i811.length; i += 1) {
    i810.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo', i811[i + 0]) );
  }
  i808.characterInfo = i810
  request.r(i809[5], i809[6], 0, i808, 'texture')
  i808.originalFontSize = i809[7]
  return i808
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo"] = function (request, data, root) {
  var i814 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo' )
  var i815 = data
  i814.index = i815[0]
  i814.advance = i815[1]
  i814.bearing = i815[2]
  i814.glyphWidth = i815[3]
  i814.glyphHeight = i815[4]
  i814.minX = i815[5]
  i814.maxX = i815[6]
  i814.minY = i815[7]
  i814.maxY = i815[8]
  i814.uvBottomLeftX = i815[9]
  i814.uvBottomLeftY = i815[10]
  i814.uvBottomRightX = i815[11]
  i814.uvBottomRightY = i815[12]
  i814.uvTopLeftX = i815[13]
  i814.uvTopLeftY = i815[14]
  i814.uvTopRightX = i815[15]
  i814.uvTopRightY = i815[16]
  return i814
}

Deserializers["Game.Data.GameConfigAsset"] = function (request, data, root) {
  var i816 = root || request.c( 'Game.Data.GameConfigAsset' )
  var i817 = data
  i816.MaxHexStackSize = i817[0]
  request.r(i817[1], i817[2], 0, i816, 'HexPrefab')
  i816.HexMoveActionDuration = i817[3]
  i816.MinHexMoveActionDuration = i817[4]
  i816.HexDesctructionActionDuration = i817[5]
  i816.MinHexDesctructionActionDuration = i817[6]
  i816.StartPlayerHexStackCount = i817[7]
  i816.PlayerHexStackLayerName = i817[8]
  i816.PlayerStackSpawnActionDuration = i817[9]
  i816.PlayerHexStackDragHeight = i817[10]
  request.r(i817[11], i817[12], 0, i816, 'GroundPrefab')
  request.r(i817[13], i817[14], 0, i816, 'GameFieldHexPrefab')
  i816.GameFieldHexHighlightColor = new pc.Color(i817[15], i817[16], i817[17], i817[18])
  i816.StackingAnimationJumpHeight = i817[19]
  i816.HexHighlightDuration = i817[20]
  request.r(i817[21], i817[22], 0, i816, 'HexDestructionEffectPrefab')
  var i819 = i817[23]
  var i818 = []
  for(var i = 0; i < i819.length; i += 1) {
    i818.push( request.d('Game.Data.HexData', i819[i + 0]) );
  }
  i816.HexesData = i818
  i816.hexStackSpawnDensity = i817[24]
  return i816
}

Deserializers["Game.Data.HexData"] = function (request, data, root) {
  var i822 = root || request.c( 'Game.Data.HexData' )
  var i823 = data
  i822.Name = i823[0]
  i822.Color = new pc.Color(i823[1], i823[2], i823[3], i823[4])
  return i822
}

Deserializers["DG.Tweening.Core.DOTweenSettings"] = function (request, data, root) {
  var i824 = root || request.c( 'DG.Tweening.Core.DOTweenSettings' )
  var i825 = data
  i824.useSafeMode = !!i825[0]
  i824.safeModeOptions = request.d('DG.Tweening.Core.DOTweenSettings+SafeModeOptions', i825[1], i824.safeModeOptions)
  i824.timeScale = i825[2]
  i824.unscaledTimeScale = i825[3]
  i824.useSmoothDeltaTime = !!i825[4]
  i824.maxSmoothUnscaledTime = i825[5]
  i824.rewindCallbackMode = i825[6]
  i824.showUnityEditorReport = !!i825[7]
  i824.logBehaviour = i825[8]
  i824.drawGizmos = !!i825[9]
  i824.defaultRecyclable = !!i825[10]
  i824.defaultAutoPlay = i825[11]
  i824.defaultUpdateType = i825[12]
  i824.defaultTimeScaleIndependent = !!i825[13]
  i824.defaultEaseType = i825[14]
  i824.defaultEaseOvershootOrAmplitude = i825[15]
  i824.defaultEasePeriod = i825[16]
  i824.defaultAutoKill = !!i825[17]
  i824.defaultLoopType = i825[18]
  i824.debugMode = !!i825[19]
  i824.debugStoreTargetId = !!i825[20]
  i824.showPreviewPanel = !!i825[21]
  i824.storeSettingsLocation = i825[22]
  i824.modules = request.d('DG.Tweening.Core.DOTweenSettings+ModulesSetup', i825[23], i824.modules)
  i824.createASMDEF = !!i825[24]
  i824.showPlayingTweens = !!i825[25]
  i824.showPausedTweens = !!i825[26]
  return i824
}

Deserializers["DG.Tweening.Core.DOTweenSettings+SafeModeOptions"] = function (request, data, root) {
  var i826 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+SafeModeOptions' )
  var i827 = data
  i826.logBehaviour = i827[0]
  i826.nestedTweenFailureBehaviour = i827[1]
  return i826
}

Deserializers["DG.Tweening.Core.DOTweenSettings+ModulesSetup"] = function (request, data, root) {
  var i828 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+ModulesSetup' )
  var i829 = data
  i828.showPanel = !!i829[0]
  i828.audioEnabled = !!i829[1]
  i828.physicsEnabled = !!i829[2]
  i828.physics2DEnabled = !!i829[3]
  i828.spriteEnabled = !!i829[4]
  i828.uiEnabled = !!i829[5]
  i828.textMeshProEnabled = !!i829[6]
  i828.tk2DEnabled = !!i829[7]
  i828.deAudioEnabled = !!i829[8]
  i828.deUnityExtendedEnabled = !!i829[9]
  i828.epoOutlineEnabled = !!i829[10]
  return i828
}

Deserializers["UnityEditor.Rendering.Universal.AssetVersion"] = function (request, data, root) {
  var i830 = root || request.c( 'UnityEditor.Rendering.Universal.AssetVersion' )
  var i831 = data
  i830.version = i831[0]
  return i830
}

Deserializers["UnityEditor.ShaderGraph.ShaderGraphMetadata"] = function (request, data, root) {
  var i832 = root || request.c( 'UnityEditor.ShaderGraph.ShaderGraphMetadata' )
  var i833 = data
  i832.outputNodeTypeName = i833[0]
  var i835 = i833[1]
  var i834 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Object')))
  for(var i = 0; i < i835.length; i += 2) {
  request.r(i835[i + 0], i835[i + 1], 1, i834, '')
  }
  i832.assetDependencies = i834
  var i837 = i833[2]
  var i836 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEditor.ShaderGraph.MinimalCategoryData')))
  for(var i = 0; i < i837.length; i += 1) {
    i836.add(request.d('UnityEditor.ShaderGraph.MinimalCategoryData', i837[i + 0]));
  }
  i832.categoryDatas = i836
  return i832
}

Deserializers["UnityEditor.ShaderGraph.MinimalCategoryData"] = function (request, data, root) {
  var i842 = root || request.c( 'UnityEditor.ShaderGraph.MinimalCategoryData' )
  var i843 = data
  i842.categoryName = i843[0]
  var i845 = i843[1]
  var i844 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEditor.ShaderGraph.GraphInputData')))
  for(var i = 0; i < i845.length; i += 1) {
    i844.add(request.d('UnityEditor.ShaderGraph.GraphInputData', i845[i + 0]));
  }
  i842.propertyDatas = i844
  return i842
}

Deserializers["UnityEditor.ShaderGraph.GraphInputData"] = function (request, data, root) {
  var i848 = root || request.c( 'UnityEditor.ShaderGraph.GraphInputData' )
  var i849 = data
  i848.referenceName = i849[0]
  i848.isKeyword = !!i849[1]
  i848.propertyType = i849[2]
  i848.keywordType = i849[3]
  i848.isCompoundProperty = !!i849[4]
  var i851 = i849[5]
  var i850 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEditor.ShaderGraph.SubPropertyData')))
  for(var i = 0; i < i851.length; i += 1) {
    i850.add(request.d('UnityEditor.ShaderGraph.SubPropertyData', i851[i + 0]));
  }
  i848.subProperties = i850
  return i848
}

Deserializers["UnityEditor.ShaderGraph.SubPropertyData"] = function (request, data, root) {
  var i854 = root || request.c( 'UnityEditor.ShaderGraph.SubPropertyData' )
  var i855 = data
  i854.referenceName = i855[0]
  i854.propertyType = i855[1]
  return i854
}

Deserializers["UnityEditor.Rendering.Universal.ShaderGraph.UniversalMetadata"] = function (request, data, root) {
  var i856 = root || request.c( 'UnityEditor.Rendering.Universal.ShaderGraph.UniversalMetadata' )
  var i857 = data
  i856.m_ShaderID = i857[0]
  i856.m_AllowMaterialOverride = !!i857[1]
  i856.m_SurfaceType = i857[2]
  i856.m_AlphaMode = i857[3]
  i856.m_CastShadows = !!i857[4]
  i856.m_IsVFXCompatible = !!i857[5]
  return i856
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i858 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i859 = data
  var i861 = i859[0]
  var i860 = []
  for(var i = 0; i < i861.length; i += 1) {
    i860.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i861[i + 0]) );
  }
  i858.files = i860
  i858.componentToPrefabIds = i859[1]
  return i858
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i864 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i865 = data
  i864.path = i865[0]
  request.r(i865[1], i865[2], 0, i864, 'unityObject')
  return i864
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i866 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i867 = data
  var i869 = i867[0]
  var i868 = []
  for(var i = 0; i < i869.length; i += 1) {
    i868.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i869[i + 0]) );
  }
  i866.scriptsExecutionOrder = i868
  var i871 = i867[1]
  var i870 = []
  for(var i = 0; i < i871.length; i += 1) {
    i870.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i871[i + 0]) );
  }
  i866.sortingLayers = i870
  var i873 = i867[2]
  var i872 = []
  for(var i = 0; i < i873.length; i += 1) {
    i872.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i873[i + 0]) );
  }
  i866.cullingLayers = i872
  i866.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i867[3], i866.timeSettings)
  i866.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i867[4], i866.physicsSettings)
  i866.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i867[5], i866.physics2DSettings)
  i866.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i867[6], i866.qualitySettings)
  i866.enableRealtimeShadows = !!i867[7]
  i866.enableAutoInstancing = !!i867[8]
  i866.enableStaticBatching = !!i867[9]
  i866.enableDynamicBatching = !!i867[10]
  i866.usePreservativeDynamicBatching = !!i867[11]
  i866.lightmapEncodingQuality = i867[12]
  i866.desiredColorSpace = i867[13]
  var i875 = i867[14]
  var i874 = []
  for(var i = 0; i < i875.length; i += 1) {
    i874.push( i875[i + 0] );
  }
  i866.allTags = i874
  return i866
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i878 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i879 = data
  i878.name = i879[0]
  i878.value = i879[1]
  return i878
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i882 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i883 = data
  i882.id = i883[0]
  i882.name = i883[1]
  i882.value = i883[2]
  return i882
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i886 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i887 = data
  i886.id = i887[0]
  i886.name = i887[1]
  return i886
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i888 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i889 = data
  i888.fixedDeltaTime = i889[0]
  i888.maximumDeltaTime = i889[1]
  i888.timeScale = i889[2]
  i888.maximumParticleTimestep = i889[3]
  return i888
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i890 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i891 = data
  i890.gravity = new pc.Vec3( i891[0], i891[1], i891[2] )
  i890.defaultSolverIterations = i891[3]
  i890.bounceThreshold = i891[4]
  i890.autoSyncTransforms = !!i891[5]
  i890.autoSimulation = !!i891[6]
  var i893 = i891[7]
  var i892 = []
  for(var i = 0; i < i893.length; i += 1) {
    i892.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i893[i + 0]) );
  }
  i890.collisionMatrix = i892
  return i890
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i896 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i897 = data
  i896.enabled = !!i897[0]
  i896.layerId = i897[1]
  i896.otherLayerId = i897[2]
  return i896
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i898 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i899 = data
  request.r(i899[0], i899[1], 0, i898, 'material')
  i898.gravity = new pc.Vec2( i899[2], i899[3] )
  i898.positionIterations = i899[4]
  i898.velocityIterations = i899[5]
  i898.velocityThreshold = i899[6]
  i898.maxLinearCorrection = i899[7]
  i898.maxAngularCorrection = i899[8]
  i898.maxTranslationSpeed = i899[9]
  i898.maxRotationSpeed = i899[10]
  i898.baumgarteScale = i899[11]
  i898.baumgarteTOIScale = i899[12]
  i898.timeToSleep = i899[13]
  i898.linearSleepTolerance = i899[14]
  i898.angularSleepTolerance = i899[15]
  i898.defaultContactOffset = i899[16]
  i898.autoSimulation = !!i899[17]
  i898.queriesHitTriggers = !!i899[18]
  i898.queriesStartInColliders = !!i899[19]
  i898.callbacksOnDisable = !!i899[20]
  i898.reuseCollisionCallbacks = !!i899[21]
  i898.autoSyncTransforms = !!i899[22]
  var i901 = i899[23]
  var i900 = []
  for(var i = 0; i < i901.length; i += 1) {
    i900.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i901[i + 0]) );
  }
  i898.collisionMatrix = i900
  return i898
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i904 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i905 = data
  i904.enabled = !!i905[0]
  i904.layerId = i905[1]
  i904.otherLayerId = i905[2]
  return i904
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i906 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i907 = data
  var i909 = i907[0]
  var i908 = []
  for(var i = 0; i < i909.length; i += 1) {
    i908.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i909[i + 0]) );
  }
  i906.qualityLevels = i908
  var i911 = i907[1]
  var i910 = []
  for(var i = 0; i < i911.length; i += 1) {
    i910.push( i911[i + 0] );
  }
  i906.names = i910
  i906.shadows = i907[2]
  i906.anisotropicFiltering = i907[3]
  i906.antiAliasing = i907[4]
  i906.lodBias = i907[5]
  i906.shadowCascades = i907[6]
  i906.shadowDistance = i907[7]
  i906.shadowmaskMode = i907[8]
  i906.shadowProjection = i907[9]
  i906.shadowResolution = i907[10]
  i906.softParticles = !!i907[11]
  i906.softVegetation = !!i907[12]
  i906.activeColorSpace = i907[13]
  i906.desiredColorSpace = i907[14]
  i906.masterTextureLimit = i907[15]
  i906.maxQueuedFrames = i907[16]
  i906.particleRaycastBudget = i907[17]
  i906.pixelLightCount = i907[18]
  i906.realtimeReflectionProbes = !!i907[19]
  i906.shadowCascade2Split = i907[20]
  i906.shadowCascade4Split = new pc.Vec3( i907[21], i907[22], i907[23] )
  i906.streamingMipmapsActive = !!i907[24]
  i906.vSyncCount = i907[25]
  i906.asyncUploadBufferSize = i907[26]
  i906.asyncUploadTimeSlice = i907[27]
  i906.billboardsFaceCameraPosition = !!i907[28]
  i906.shadowNearPlaneOffset = i907[29]
  i906.streamingMipmapsMemoryBudget = i907[30]
  i906.maximumLODLevel = i907[31]
  i906.streamingMipmapsAddAllCameras = !!i907[32]
  i906.streamingMipmapsMaxLevelReduction = i907[33]
  i906.streamingMipmapsRenderersPerFrame = i907[34]
  i906.resolutionScalingFixedDPIFactor = i907[35]
  i906.streamingMipmapsMaxFileIORequests = i907[36]
  i906.currentQualityLevel = i907[37]
  return i906
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame"] = function (request, data, root) {
  var i916 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame' )
  var i917 = data
  i916.weight = i917[0]
  i916.vertices = i917[1]
  i916.normals = i917[2]
  i916.tangents = i917[3]
  return i916
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i918 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i919 = data
  request.r(i919[0], i919[1], 0, i918, 'm_ObjectArgument')
  i918.m_ObjectArgumentAssemblyTypeName = i919[2]
  i918.m_IntArgument = i919[3]
  i918.m_FloatArgument = i919[4]
  i918.m_StringArgument = i919[5]
  i918.m_BoolArgument = !!i919[6]
  return i918
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+RenderObjectsSettings"] = function (request, data, root) {
  var i920 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+RenderObjectsSettings' )
  var i921 = data
  i920.Event = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i921[0], i920.Event)
  i920.filterSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+FilterSettings', i921[1], i920.filterSettings)
  i920.overrideMaterialId = i921[2]
  i920.overrideMaterialPassIndex = i921[3]
  i920.overrideShaderId = i921[4]
  i920.overrideShaderPassIndex = i921[5]
  i920.overrideMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i921[6], i920.overrideMode)
  i920.overrideDepthState = !!i921[7]
  i920.depthCompareFunction = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i921[8], i920.depthCompareFunction)
  i920.enableWrite = !!i921[9]
  i920.stencilSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.StencilStateData', i921[10], i920.stencilSettings)
  i920.cameraSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+CustomCameraSettings', i921[11], i920.cameraSettings)
  return i920
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.EnumDescription"] = function (request, data, root) {
  var i922 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.EnumDescription' )
  var i923 = data
  i922.Value = i923[0]
  return i922
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+FilterSettings"] = function (request, data, root) {
  var i924 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+FilterSettings' )
  var i925 = data
  i924.RenderQueueType = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i925[0], i924.RenderQueueType)
  i924.LayerMask = i925[1]
  var i927 = i925[2]
  var i926 = []
  for(var i = 0; i < i927.length; i += 1) {
    i926.push( i927[i + 0] );
  }
  i924.PassNames = i926
  return i924
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.StencilStateData"] = function (request, data, root) {
  var i928 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.StencilStateData' )
  var i929 = data
  i928.overrideStencilState = !!i929[0]
  i928.stencilReference = i929[1]
  i928.stencilCompareFunctionValue = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i929[2], i928.stencilCompareFunctionValue)
  i928.passOperationValue = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i929[3], i928.passOperationValue)
  i928.failOperationValue = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i929[4], i928.failOperationValue)
  i928.zFailOperationValue = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i929[5], i928.zFailOperationValue)
  return i928
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+CustomCameraSettings"] = function (request, data, root) {
  var i930 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+CustomCameraSettings' )
  var i931 = data
  i930.overrideCamera = !!i931[0]
  i930.restoreCamera = !!i931[1]
  i930.offset = new pc.Vec4( i931[2], i931[3], i931[4], i931[5] )
  i930.cameraFieldOfView = i931[6]
  return i930
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Components.Transform":{"position":0,"scale":3,"rotation":6},"Luna.Unity.DTO.UnityEngine.Components.MeshFilter":{"sharedMesh":0},"Luna.Unity.DTO.UnityEngine.Components.MeshRenderer":{"additionalVertexStreams":0,"enabled":2,"sharedMaterial":3,"sharedMaterials":5,"receiveShadows":6,"shadowCastingMode":7,"sortingLayerID":8,"sortingOrder":9,"lightmapIndex":10,"lightmapSceneIndex":11,"lightmapScaleOffset":12,"lightProbeUsage":16,"reflectionProbeUsage":17},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Assets.Mesh":{"name":0,"halfPrecision":1,"useSimplification":2,"useUInt32IndexFormat":3,"vertexCount":4,"aabb":5,"streams":6,"vertices":7,"subMeshes":8,"bindposes":9,"blendShapes":10},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh":{"triangles":0},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape":{"name":0,"frames":1},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystem":{"main":0,"colorBySpeed":1,"colorOverLifetime":2,"emission":3,"rotationBySpeed":4,"rotationOverLifetime":5,"shape":6,"sizeBySpeed":7,"sizeOverLifetime":8,"textureSheetAnimation":9,"velocityOverLifetime":10,"noise":11,"inheritVelocity":12,"forceOverLifetime":13,"limitVelocityOverLifetime":14,"useAutoRandomSeed":15,"randomSeed":16},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule":{"duration":0,"loop":1,"prewarm":2,"startDelay":3,"startLifetime":4,"startSpeed":5,"startSize3D":6,"startSizeX":7,"startSizeY":8,"startSizeZ":9,"startRotation3D":10,"startRotationX":11,"startRotationY":12,"startRotationZ":13,"startColor":14,"gravityModifier":15,"simulationSpace":16,"customSimulationSpace":17,"simulationSpeed":19,"useUnscaledTime":20,"scalingMode":21,"playOnAwake":22,"maxParticles":23,"emitterVelocityMode":24,"stopAction":25},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve":{"mode":0,"curveMin":1,"curveMax":2,"curveMultiplier":3,"constantMin":4,"constantMax":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient":{"mode":0,"gradientMin":1,"gradientMax":2,"colorMin":3,"colorMax":7},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient":{"mode":0,"colorKeys":1,"alphaKeys":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule":{"enabled":0,"color":1,"range":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey":{"color":0,"time":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey":{"alpha":0,"time":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule":{"enabled":0,"color":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule":{"enabled":0,"rateOverTime":1,"rateOverDistance":2,"bursts":3},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst":{"count":0,"cycleCount":1,"minCount":2,"maxCount":3,"repeatInterval":4,"time":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule":{"enabled":0,"shapeType":1,"randomDirectionAmount":2,"sphericalDirectionAmount":3,"randomPositionAmount":4,"alignToDirection":5,"radius":6,"radiusMode":7,"radiusSpread":8,"radiusSpeed":9,"radiusThickness":10,"angle":11,"length":12,"boxThickness":13,"meshShapeType":16,"mesh":17,"meshRenderer":19,"skinnedMeshRenderer":21,"useMeshMaterialIndex":23,"meshMaterialIndex":24,"useMeshColors":25,"normalOffset":26,"arc":27,"arcMode":28,"arcSpread":29,"arcSpeed":30,"donutRadius":31,"position":32,"rotation":35,"scale":38},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule":{"enabled":0,"mode":1,"animation":2,"numTilesX":3,"numTilesY":4,"useRandomRow":5,"frameOverTime":6,"startFrame":7,"cycleCount":8,"rowIndex":9,"flipU":10,"flipV":11,"spriteCount":12,"sprites":13},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"radial":4,"speedModifier":5,"space":6,"orbitalX":7,"orbitalY":8,"orbitalZ":9,"orbitalOffsetX":10,"orbitalOffsetY":11,"orbitalOffsetZ":12},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule":{"enabled":0,"separateAxes":1,"strengthX":2,"strengthY":3,"strengthZ":4,"frequency":5,"damping":6,"octaveCount":7,"octaveMultiplier":8,"octaveScale":9,"quality":10,"scrollSpeed":11,"scrollSpeedMultiplier":12,"remapEnabled":13,"remapX":14,"remapY":15,"remapZ":16,"positionAmount":17,"rotationAmount":18,"sizeAmount":19},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule":{"enabled":0,"mode":1,"curve":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"space":4,"randomized":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule":{"enabled":0,"limit":1,"limitX":2,"limitY":3,"limitZ":4,"dampen":5,"separateAxes":6,"space":7,"drag":8,"multiplyDragByParticleSize":9,"multiplyDragByParticleVelocity":10},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer":{"mesh":0,"meshCount":2,"activeVertexStreamsCount":3,"alignment":4,"renderMode":5,"sortMode":6,"lengthScale":7,"velocityScale":8,"cameraVelocityScale":9,"normalDirection":10,"sortingFudge":11,"minParticleSize":12,"maxParticleSize":13,"pivot":14,"trailMaterial":17,"applyActiveColorSpace":19,"enabled":20,"sharedMaterial":21,"sharedMaterials":23,"receiveShadows":24,"shadowCastingMode":25,"sortingLayerID":26,"sortingOrder":27,"lightmapIndex":28,"lightmapSceneIndex":29,"lightmapScaleOffset":30,"lightProbeUsage":34,"reflectionProbeUsage":35},"Luna.Unity.DTO.UnityEngine.Components.RectTransform":{"pivot":0,"anchorMin":2,"anchorMax":4,"sizeDelta":6,"anchoredPosition3D":8,"rotation":11,"scale":15},"Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer":{"cullTransparentMesh":0},"Luna.Unity.DTO.UnityEngine.Components.CanvasGroup":{"m_Alpha":0,"m_Interactable":1,"m_BlocksRaycasts":2,"m_IgnoreParentGroups":3,"enabled":4},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Components.Canvas":{"planeDistance":0,"referencePixelsPerUnit":1,"isFallbackOverlay":2,"renderMode":3,"renderOrder":4,"sortingLayerName":5,"sortingOrder":6,"scaleFactor":7,"worldCamera":8,"overrideSorting":10,"pixelPerfect":11,"targetDisplay":12,"overridePixelPerfect":13,"enabled":14},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"aspect":0,"orthographic":1,"orthographicSize":2,"backgroundColor":3,"nearClipPlane":7,"farClipPlane":8,"fieldOfView":9,"depth":10,"clearFlags":11,"cullingMask":12,"rect":13,"targetTexture":14,"usePhysicalProperties":16,"focalLength":17,"sensorSize":18,"lensShift":20,"gateFit":22,"commandBufferCount":23,"cameraType":24,"enabled":25},"Luna.Unity.DTO.UnityEngine.Components.Light":{"type":0,"color":1,"cullingMask":5,"intensity":6,"range":7,"spotAngle":8,"shadows":9,"shadowNormalBias":10,"shadowBias":11,"shadowStrength":12,"shadowResolution":13,"lightmapBakeType":14,"renderMode":15,"cookie":16,"cookieSize":18,"shadowNearPlane":19,"occlusionMaskChannel":20,"isBaked":21,"mixedLightingMode":22,"enabled":23},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"customReflection":36,"defaultReflection":38,"defaultReflectionMode":40,"defaultReflectionResolution":41,"sunLightObjectId":42,"pixelLightCount":43,"defaultReflectionHDR":44,"hasLightDataAsset":45,"hasManualGenerate":46},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2,"shadowMask":4},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset":{"AdditionalLightsRenderingMode":0,"LightRenderingMode":1,"MainLightRenderingModeValue":2,"SupportsMainLightShadows":3,"MixedLightingSupported":4,"MainLightShadowmapResolutionValue":5,"SupportsSoftShadows":6,"SoftShadowQualityValue":7,"ShadowDistance":8,"ShadowCascadeCount":9,"Cascade2Split":10,"Cascade3Split":11,"Cascade4Split":13,"CascadeBorder":16,"ShadowDepthBias":17,"ShadowNormalBias":18,"RequireDepthTexture":19,"RequireOpaqueTexture":20,"scriptableRendererData":21},"Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode":{"Disabled":0,"PerVertex":1,"PerPixel":2},"Luna.Unity.DTO.UnityEngine.Assets.ScriptableRendererData":{"opaqueLayerMask":0,"transparentLayerMask":1,"RenderObjectsFeatures":2,"name":3},"Luna.Unity.DTO.UnityEngine.Assets.RenderObjects":{"settings":0,"name":1,"typeName":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"hasDepthOnlyPass":10,"isCreatedByShaderGraph":11,"disableBatching":12,"compiled":13},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Textures.Sprite":{"name":0,"texture":1,"aabb":3,"vertices":4,"triangles":5,"textureRect":6,"packedRect":10,"border":14,"transparency":18,"bounds":19,"pixelsPerUnit":20,"textureWidth":21,"textureHeight":22,"nativeSize":23,"pivot":25,"textureRectOffset":27},"Luna.Unity.DTO.UnityEngine.Assets.Font":{"name":0,"ascent":1,"originalLineHeight":2,"fontSize":3,"characterInfo":4,"texture":5,"originalFontSize":7},"Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo":{"index":0,"advance":1,"bearing":2,"glyphWidth":3,"glyphHeight":4,"minX":5,"maxX":6,"minY":7,"maxY":8,"uvBottomLeftX":9,"uvBottomLeftY":10,"uvBottomRightX":11,"uvBottomRightY":12,"uvTopLeftX":13,"uvTopLeftY":14,"uvTopRightX":15,"uvTopRightY":16},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableStaticBatching":9,"enableDynamicBatching":10,"usePreservativeDynamicBatching":11,"lightmapEncodingQuality":12,"desiredColorSpace":13,"allTags":14},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame":{"weight":0,"vertices":1,"normals":2,"tangents":3},"Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+RenderObjectsSettings":{"Event":0,"filterSettings":1,"overrideMaterialId":2,"overrideMaterialPassIndex":3,"overrideShaderId":4,"overrideShaderPassIndex":5,"overrideMode":6,"overrideDepthState":7,"depthCompareFunction":8,"enableWrite":9,"stencilSettings":10,"cameraSettings":11},"Luna.Unity.DTO.UnityEngine.Assets.EnumDescription":{"Value":0},"Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+FilterSettings":{"RenderQueueType":0,"LayerMask":1,"PassNames":2},"Luna.Unity.DTO.UnityEngine.Assets.StencilStateData":{"overrideStencilState":0,"stencilReference":1,"stencilCompareFunctionValue":2,"passOperationValue":3,"failOperationValue":4,"zFailOperationValue":5},"Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+CustomCameraSettings":{"overrideCamera":0,"restoreCamera":1,"offset":2,"cameraFieldOfView":6}}

Deserializers.requiredComponents = {"44":[45],"46":[45],"47":[45],"48":[45],"49":[45],"50":[45],"51":[52],"53":[32],"54":[55],"56":[55],"57":[55],"58":[55],"59":[55],"60":[55],"61":[55],"62":[63],"64":[63],"65":[63],"66":[63],"67":[63],"68":[63],"69":[63],"70":[63],"71":[63],"72":[63],"73":[63],"74":[63],"75":[63],"76":[32],"77":[3],"78":[79],"80":[79],"29":[12],"81":[12],"82":[32],"34":[32],"36":[35],"83":[84],"85":[12],"86":[12],"31":[29],"14":[15,12],"87":[12],"30":[29],"88":[12],"89":[12],"90":[12],"91":[12],"92":[12],"93":[12],"94":[12],"95":[12],"96":[12],"97":[15,12],"98":[12],"99":[12],"100":[12],"101":[12],"22":[15,12],"102":[12],"103":[37],"104":[37],"38":[37],"105":[37],"106":[32],"107":[32],"108":[84],"109":[12],"110":[3,12],"111":[12,15],"112":[12],"113":[15,12],"114":[3],"115":[15,12],"116":[12],"117":[84]}

Deserializers.types = ["UnityEngine.Transform","UnityEngine.MeshFilter","UnityEngine.Mesh","UnityEngine.MeshRenderer","UnityEngine.Material","UnityEngine.MonoBehaviour","Game.Entities.HexComponent","UnityEngine.Shader","UnityEngine.Texture2D","UnityEngine.ParticleSystem","UnityEngine.ParticleSystemRenderer","Game.World.HexGridComponent","UnityEngine.RectTransform","Game.UI.TutorialScreenViewComponent","UnityEngine.UI.Image","UnityEngine.CanvasRenderer","UnityEngine.EventSystems.UIBehaviour","UnityEngine.Sprite","Game.UI.GameEndScreenViewComponent","UnityEngine.UI.Button","UnityEngine.CanvasGroup","UnityEngine.UI.Outline","UnityEngine.UI.Text","UnityEngine.Font","UnityEngine.UI.Shadow","Zenject.SceneContext","Game.Bootstrap.GameRunInstaller","Game.Bootstrap.GameUIInstaller","Game.Data.GameConfigAsset","UnityEngine.Canvas","UnityEngine.UI.CanvasScaler","UnityEngine.UI.GraphicRaycaster","UnityEngine.Camera","UnityEngine.AudioListener","UnityEngine.Rendering.Universal.UniversalAdditionalCameraData","UnityEngine.Light","UnityEngine.Rendering.Universal.UniversalAdditionalLightData","UnityEngine.EventSystems.EventSystem","UnityEngine.EventSystems.StandaloneInputModule","UnityEngine.GameObject","DG.Tweening.Core.DOTweenSettings","UnityEditor.Rendering.Universal.AssetVersion","UnityEditor.ShaderGraph.ShaderGraphMetadata","UnityEditor.Rendering.Universal.ShaderGraph.UniversalMetadata","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.ConstantForce","UnityEngine.Rigidbody","UnityEngine.Joint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.FixedJoint","UnityEngine.CharacterJoint","UnityEngine.ConfigurableJoint","UnityEngine.CompositeCollider2D","UnityEngine.Rigidbody2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","UnityEngine.Rendering.UI.UIFoldout","UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera","Unity.VisualScripting.SceneVariables","Unity.VisualScripting.Variables","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.RectMask2D","UnityEngine.UI.Scrollbar","UnityEngine.UI.ScrollRect","UnityEngine.UI.Slider","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster","Unity.VisualScripting.ScriptMachine","TMPro.TextContainer","TMPro.TextMeshPro","TMPro.TextMeshProUGUI","TMPro.TMP_Dropdown","TMPro.TMP_SelectionCaret","TMPro.TMP_SubMesh","TMPro.TMP_SubMeshUI","TMPro.TMP_Text","Unity.VisualScripting.StateMachine"]

Deserializers.unityVersion = "2022.3.62f3";

Deserializers.productName = "BeNiceGames_TestTask";

Deserializers.lunaInitializationTime = "05/06/2026 18:09:29";

Deserializers.lunaDaysRunning = "0.3";

Deserializers.lunaVersion = "7.2.0";

Deserializers.lunaSHA = "ea08d29afe2968efcb8d91d5624f033c6485cc68";

Deserializers.creativeName = "Hexes";

Deserializers.lunaAppID = "39247";

Deserializers.projectId = "a03561e1f55580f458310c012613bedc";

Deserializers.packagesInfo = "com.unity.render-pipelines.universal: 14.0.12\ncom.unity.textmeshpro: 3.0.7";

Deserializers.externalJsLibraries = "";

Deserializers.androidLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.androidLink?window.$environment.packageConfig.androidLink:'Empty';

Deserializers.iosLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.iosLink?window.$environment.packageConfig.iosLink:'Empty';

Deserializers.base64Enabled = "False";

Deserializers.minifyEnabled = "True";

Deserializers.isForceUncompressed = "False";

Deserializers.isAntiAliasingEnabled = "False";

Deserializers.isRuntimeAnalysisEnabledForCode = "False";

Deserializers.runtimeAnalysisExcludedClassesCount = "1867";

Deserializers.runtimeAnalysisExcludedMethodsCount = "5835";

Deserializers.runtimeAnalysisExcludedModules = "mecanim-wasm";

Deserializers.isRuntimeAnalysisEnabledForShaders = "True";

Deserializers.isRealtimeShadowsEnabled = "True";

Deserializers.isLunaCompilerV2Used = "True";

Deserializers.companyName = "alexmk";

Deserializers.buildPlatform = "StandaloneWindows64";

Deserializers.applicationIdentifier = "com.Unity-Technologies.com.unity.template.urp-blank";

Deserializers.disableAntiAliasing = true;

Deserializers.graphicsConstraint = 24;

Deserializers.linearColorSpace = false;

Deserializers.buildID = "521244c9-9aa2-4692-8222-f26b59a93406";

Deserializers.runtimeInitializeOnLoadInfos = [[["UnityEngine","Rendering","DebugUpdater","RuntimeInit"],["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[["Unity","VisualScripting","RuntimeVSUsageUtility","RuntimeInitializeOnLoadBeforeSceneLoad"]],[],[["UnityEngine","Experimental","Rendering","XRSystem","XRSystemInit"]],[]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

