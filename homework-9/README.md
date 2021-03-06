# Unity-3D-homework9
-----

#  坦克对战游戏 AI 

----

视频地址：https://pan.baidu.com/s/1JjvXQL5aNfZR0mum6v9UPA

参考师兄博客：http://www.chenxd59.cn/?p=213

------
## 要求：
- 使用“感知-思考-行为”模型，建模 AI 坦克
- 场景中要放置一些障碍阻挡对手视线
- 坦克需要放置一个矩阵包围盒触发器，以保证 AI 坦克能使用射线探测对手方位
- AI 坦克必须在有目标条件下使用导航，并能绕过障碍。（失去目标时策略自己思考）
- 实现人机对战

-----

## 游戏实现：
- 实现效果：AI会自动寻路，玩家可以移动来躲避AI的追捕，或者发射子弹来击走敌人。

### Tanks! Tutorial
- Assets store 里免费的坦克大战游戏制作教程，里面有丰富的游戏资源供我们选择使用

### 感知-思考-行为 模型
- AI的实现主要在“思考”这一部分，这也是坦克AI的核心所在
- 要实现地方坦克的自动寻路，我们需要使用到unity中的NavMesh。
- NavMesh入门教程：  
    http://liweizhaolili.blog.163.com/blog/static/16230744201271161310135/
    https://blog.csdn.net/zzj051319/article/details/71687833

### 地图的烘焙
- 要使用NavMesh我们首先需要通过烘培生成一个可以被NavAgent识别的地形。
    
    创建地形如下：

    ![1](https://img1.doubanio.com/view/photo/l/public/p2526271748.webp)

    接着进行烘焙：

    ![2](https://img3.doubanio.com/view/photo/l/public/p2526271745.webp)
    
    ![3](https://img1.doubanio.com/view/photo/l/public/p2526271747.webp)