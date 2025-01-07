# Guilherme Raiol Coelho Batalha - Match-3

![Match-3](/Match3.png?raw=true "Match-3")

Developed in Unity 2022.3.55f1

## Links
- https://unity.com/releases/editor/archive

## Minha versao
![image](https://github.com/user-attachments/assets/2eed0bad-e155-4e2b-9e72-4aaa1e04f4e9)

## O que foi feito:

- Tema realizado: Board game de madeira.
- Simples Gameplay loop para testes de todos os recursos.
- Tres efeitos principais. Um simples para combinacoes comuns de 3 tiles. Um para combinacoes de 4 tiles. E um efeito para 7 ou mais tiles combinados ao mesmo tempo.
- Efeito de tela tremendo em combinacoes para intensidade dos efeitos.
- Posicionamento de efeitos e correcao de timing para as tiles.
- Mudanca no sistema de geracao de tiles: Troca de instanciamento de multiplos prefabs diferentes para um unico prefab com possibilidade de customizacao.
- Animacoes na tela de menu, abertura de telas e transicao para cena de gameplay. Também animacoes para entrada na tela de gameplay e na saida, para transicionar ao menu.
- Texturas simples, com tratamento de tamanho de arquivo na unity, tambem seguindo padroes.
- Criacao e otimizacao de SpriteAtlas.
- Criacao de Shaders para efeitos visuais com otimizacao de GPU Instancing.
- Ajuste de tela para 16:9, 18:9 e 4:3. Efeitos seguem o tamanho (particle systems não escalam com UI).
- Criacao de sistema de Pooling para otimizacao de memoria e quadros por segundo.
- Criacao de sistema de Events para chamada de efeitos visuais.
- Reducao de Branches e DrawCalls.
