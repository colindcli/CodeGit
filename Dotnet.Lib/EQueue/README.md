> EQueue

**架构说明：**

 1. 总共有Producer, Consumer, Broker, Name Server四种服务器角色；
 2. Name Server的职责是负责管理所有的Broker，并为Producer，Consumer提供Broker信息以及所有Topic的路由信息；
 3. 从部署逻辑上看，Broker Master, Broker Slave是属于一个逻辑上的单元，一个Broker Master可以配置多个Broker Slave；所以，我设计了一个Broker Group的概念。同一个Broker Group中可以有一个Broker Master和多个Broker Slave；
 4. Broker启动时，
- 与配置的所有的Name Server建立TCP长连接；
- 定时（5s，可配置）向所有的Name Server注册自己的所有信息，主要包括：基本信息、队列信息、消费信息、生成者信息、消费者信息；
 5. Name Server之间无联系，数据无同步；Name Server也可以部署多台，由于每台Broker都会向所有的Name Server注册自己的信息，所以，理论上所有的Name Server里维护的信息最终都是完全一致的；Name Server不持久化任何东西，启动后只在内存中维护所有Broker上报上来的信息；Name Server不与其他任何服务器主动通信；
 6. Broker Slave会从Broker Master通过拉的方式同步消息，并存储到本地磁盘，消息同步为异步同步；
 7. Producer启动时，
- 与配置的所有Name Server服务器建立TCP长连接；
- 随机选择一台Name Server获取所有可用的Broker列表，对所有的Broker建立TCP长连接，并定时（5s，可配置）更新所有可用的Broker列表；
- 定时（1s，可配置）向所有当前连接的Broker发送心跳，将自己的信息注册到Broker；
- 定时（5s，可配置）从Name Server获取所有当前集群的所有Topic的队列信息；
- 发送Topic时，如果该Topic的队列信息在本地存在，则直接从本地获取队列信息；如果不存在，则尝试从Name Server获取，如果Name Server上获取不了，则认为该Topic下没有队列信息；如果没有获取到队列信息，则会重试这个步骤5次（可配置），以保证尽量能发送消息成功；
 8. Consumer启动时，
- 与配置的所有Name Server服务器建立TCP长连接；
- 随机选择一台Name Server获取所有可用的Broker列表，对所有的Broker建立TCP长连接，并定时（5s，可配置）更新所有可用的Broker列表；
- 定时（1s，可配置）向所有当前连接的Broker发送心跳，将自己的信息注册到Broker；
- 定时（5s，可配置）从Name Server获取所有当前集群的所有Topic的队列信息；
- 定时（每隔1s，可配置）进行消费者负载均衡，消费者负载均衡的逻辑是，针对当前消费者订阅的每个Topic，执行下面的逻辑：
A: 从本地获取该Topic的所有队列信息；
B: 从Broker集群中的第一台启动的并且可用的Broker获取所有当前在线的消费者；
C: 根据获取到的队列和消费者信息，按队列个数平均的目的为算法，为消费者平均分配队列，完成消费者负载均衡的目的；
 9. Broker的Producer心跳超时时间默认为10s；Broker的Consumer心跳超时时间默认为10s；Name Server的Broker超时时间未10s；
