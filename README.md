# GhostBoy

Ghost Boy is Unity platform 2D game

![Image](https://github.com/user-attachments/assets/88f47a58-9267-4984-85c7-d31b365f6fee)

# Goal

Goal of the game is to move the player through obsticles and stay alive and kill some mobs until u finish the level

# Previews 

![Image](https://github.com/user-attachments/assets/3230245b-7665-4689-afda-0402f12b7637)

Checkpoints

![Image](https://github.com/user-attachments/assets/7c4b6154-794f-4e52-be2a-bd19546953e1)

Collecting hearts and fighting mobs

![Image](https://github.com/user-attachments/assets/a77b34fd-b2b1-4cd1-94b0-a7b27ce47d54)

![image](https://github.com/user-attachments/assets/8da4dd95-fa93-445d-892a-6deb646c8ef5)

![image](https://github.com/user-attachments/assets/dc34ca3c-2a17-4c17-ab14-99f29f08af1a)

Death screen where u can restart or close the game 

![image](https://github.com/user-attachments/assets/c416a861-e15d-489c-b7f8-5cd92f9cf1e9)

# Player Movement/Jumping Method

horizontalInput = Input.GetAxis("Horizontal");


        //Flip player when moving left/right
        if(horizontalInput > 0.01f){
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //animator
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Jump
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        //Adjustable jump height
        if(Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0){
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
        }
        if(onWall()){
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else{
            body.gravityScale = 7;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if(isGrounded()){
                coyoteCounter = coyoteTime; //reset coyote counter when on the ground
            }
            else{
                coyoteCounter -= Time.deltaTime; //stars decreasing coyote counter when not on the ground
            }
        }


