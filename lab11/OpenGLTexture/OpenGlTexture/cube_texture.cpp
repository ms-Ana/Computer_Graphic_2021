#include <gl/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>

#include <iostream>


// Переменные с индентификаторами ID
// ID шейдерной программы
GLuint shaderProgram;
// ID атрибута вершин
GLint attribVertex;
// ID атрибута текстурных координат
GLint attribTexture;
// ID юниформа текстуры
GLint unifTexture;
// ID юниформа угла поворота
GLint unifAngle;
// ID буфера ыершин
GLuint vertexVBO;
// ID буфера текстурных координат
GLuint textureVBO;
// ID текстуры
GLint textureHandle;
// SFML текстура
sf::Texture textureData;


// Вершина
struct Vertex
{
    GLfloat x;
    GLfloat y;
    GLfloat z;
};

float angle[2] = { 0.0f, 0.0f };

const char* VertexShaderSource = R"(
    #version 330 core

    uniform vec2 angle;

    in vec3 vertCoord;
    in vec2 texureCoord;

    out vec2 tCoord;

    void main() {
        tCoord = texureCoord;
        vec3 position = vertCoord * mat3(
            1, 0, 0,
            0, cos(angle[0]), -sin(angle[0]),
            0, sin(angle[0]), cos(angle[0])
        ) * mat3(
            cos(angle[1]), 0, sin(angle[1]),
            0, 1, 0,
            -sin(angle[1]), 0, cos(angle[1])
        );
        gl_Position = vec4(position, 1.0);
    }
)";

const char* FragShaderSource = R"(
    #version 330 core

    uniform sampler2D textureData;
    in vec2 tCoord;
    out vec4 color;

    void main() {
        color = texture(textureData, tCoord);
    }
)";


void Init();
void Draw();
void Release();


int main() {
    sf::Window window(sf::VideoMode(600, 600), "My OpenGL window", sf::Style::Default, sf::ContextSettings(24));
    window.setVerticalSyncEnabled(true);

    window.setActive(true);

    glewInit();

    Init();

    glEnable(GL_DEPTH_TEST);
    glDepthFunc(GL_LESS);

    while (window.isOpen()) {
        sf::Event event;
        while (window.pollEvent(event)) {
            if (event.type == sf::Event::Closed) {
                window.close();
            }
            else if (event.type == sf::Event::Resized) {
                glViewport(0, 0, event.size.width, event.size.height);
            }
            // обработка нажатий клавиш
            else if (event.type == sf::Event::KeyPressed) {
                switch (event.key.code) {
                case (sf::Keyboard::Left): angle[1] += 0.1; break;
                case (sf::Keyboard::Right): angle[1] -= 0.1; break;
                case (sf::Keyboard::Down): angle[0] += 0.1; break;
                case (sf::Keyboard::Up): angle[0] -= 0.1; break;
                default: break;
                }
            }
        }

        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        Draw();

        window.display();
    }

    Release();
    return 0;
}


// Проверка ошибок OpenGL, если есть то вывод в консоль тип ошибки
void checkOpenGLerror() {
    GLenum errCode;
    // Коды ошибок можно смотреть тут
    // https://www.khronos.org/opengl/wiki/OpenGL_Error
    if ((errCode = glGetError()) != GL_NO_ERROR)
        std::cout << "OpenGl error!: " << errCode << std::endl;
}

// Функция печати лога шейдера
void ShaderLog(unsigned int shader)
{
    int infologLen = 0;
    int charsWritten = 0;
    char* infoLog;
    glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &infologLen);
    if (infologLen > 1)
    {
        infoLog = new char[infologLen];
        if (infoLog == NULL)
        {
            std::cout << "ERROR: Could not allocate InfoLog buffer" << std::endl;
            exit(1);
        }
        glGetShaderInfoLog(shader, infologLen, &charsWritten, infoLog);
        std::cout << "InfoLog: " << infoLog << "\n\n\n";
        delete[] infoLog;
    }
}


void InitVBO()
{
    glGenBuffers(1, &vertexVBO);
    glGenBuffers(1, &textureVBO);

    Vertex triangle[36] = {
        { -0.5, -0.5, +0.5 }, { -0.5, +0.5, +0.5 }, { +0.5, +0.5, +0.5 },
        { +0.5, +0.5, +0.5 }, { +0.5, -0.5, +0.5 }, { -0.5, -0.5, +0.5 },
        { -0.5, -0.5, -0.5 }, { +0.5, +0.5, -0.5 }, { -0.5, +0.5, -0.5 },
        { +0.5, +0.5, -0.5 }, { -0.5, -0.5, -0.5 }, { +0.5, -0.5, -0.5 },

        { -0.5, +0.5, -0.5 }, { -0.5, +0.5, +0.5 }, { +0.5, +0.5, +0.5 },
        { +0.5, +0.5, +0.5 }, { +0.5, +0.5, -0.5 }, { -0.5, +0.5, -0.5 },
        { -0.5, -0.5, -0.5 }, { +0.5, -0.5, +0.5 }, { -0.5, -0.5, +0.5 },
        { +0.5, -0.5, +0.5 }, { -0.5, -0.5, -0.5 }, { +0.5, -0.5, -0.5 },

        { +0.5, -0.5, -0.5 }, { +0.5, -0.5, +0.5 }, { +0.5, +0.5, +0.5 },
        { +0.5, +0.5, +0.5 }, { +0.5, +0.5, -0.5 }, { +0.5, -0.5, -0.5 },
        { -0.5, -0.5, -0.5 }, { -0.5, +0.5, +0.5 }, { -0.5, -0.5, +0.5 },
        { -0.5, +0.5, +0.5 }, { -0.5, -0.5, -0.5 }, { -0.5, +0.5, -0.5 }
    };

    // Объявляем текстурные координаты
    Vertex texture[36] = {
         0.0f, 0.0f,  0.0f, 1.0f,  1.0f, 1.0f,
         1.0f, 1.0f,  1.0f, 0.0f,  0.0f, 0.0f,
         1.0f, 1.0f,  0.0f, 0.0f,  0.0f, 1.0f,
         0.0f, 0.0f,  1.0f, 1.0f,  1.0f, 0.0f,

         0.0f, 0.0f,  0.0f, 1.0f,  1.0f, 1.0f,
         1.0f, 1.0f,  1.0f, 0.0f,  0.0f, 0.0f,
         1.0f, 1.0f,  0.0f, 0.0f,  0.0f, 1.0f,
         0.0f, 0.0f,  1.0f, 1.0f,  1.0f, 0.0f,

         0.0f, 0.0f,  0.0f, 1.0f,  1.0f, 1.0f,
         1.0f, 1.0f,  1.0f, 0.0f,  0.0f, 0.0f,
         1.0f, 1.0f,  0.0f, 0.0f,  0.0f, 1.0f,
         0.0f, 0.0f,  1.0f, 1.0f,  1.0f, 0.0f
    };

    glBindBuffer(GL_ARRAY_BUFFER, vertexVBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(triangle), triangle, GL_STATIC_DRAW);
    glBindBuffer(GL_ARRAY_BUFFER, textureVBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(texture), texture, GL_STATIC_DRAW);
    checkOpenGLerror();
}


void InitShader() {
    GLuint vShader = glCreateShader(GL_VERTEX_SHADER);
    glShaderSource(vShader, 1, &VertexShaderSource, NULL);
    glCompileShader(vShader);
    std::cout << "vertex shader \n";
    ShaderLog(vShader);

    GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);
    glShaderSource(fShader, 1, &FragShaderSource, NULL);
    glCompileShader(fShader);
    std::cout << "fragment shader \n";
    ShaderLog(fShader);

    shaderProgram = glCreateProgram();
    glAttachShader(shaderProgram, vShader);
    glAttachShader(shaderProgram, fShader);

    glLinkProgram(shaderProgram);
    int link_status;
    glGetProgramiv(shaderProgram, GL_LINK_STATUS, &link_status);
    if (!link_status)
    {
        std::cout << "error attach shaders \n";
        return;
    }

    attribVertex = glGetAttribLocation(shaderProgram, "vertCoord");
    if (attribVertex == -1)
    {
        std::cout << "could not bind attrib vertCoord" << std::endl;
        return;
    }

    attribTexture = glGetAttribLocation(shaderProgram, "texureCoord");
    if (attribTexture == -1)
    {
        std::cout << "could not bind attrib texureCoord" << std::endl;
        return;
    }

    unifTexture = glGetUniformLocation(shaderProgram, "textureData");
    if (unifTexture == -1)
    {
        std::cout << "could not bind uniform textureData" << std::endl;
        return;
    }

    unifAngle = glGetUniformLocation(shaderProgram, "angle");
    if (unifAngle == -1)
    {
        std::cout << "could not bind uniform angle" << std::endl;
        return;
    }
    checkOpenGLerror();
}

void InitTexture()
{
    const char* filename = "cat.jpg";

    // Загружаем текстуру из файла
    if (!textureData.loadFromFile(filename))
    {
        // Не вышло загрузить картинку
        return;
    }
    // Теперь получаем openGL дескриптор текстуры
    textureHandle = textureData.getNativeHandle();
}

void Init() {
    InitShader();
    InitVBO();
    InitTexture();
}


void Draw() {
    // Устанавливаем шейдерную программу текущей
    glUseProgram(shaderProgram);
    // Передаем юниформ в шейдер
    glUniform2fv(unifAngle, 1, angle);
    
    // Активируем текстурный блок 0, делать этого не обязательно, по умолчанию
    // и так активирован GL_TEXTURE0, это нужно для использования нескольких текстур
    glActiveTexture(GL_TEXTURE0);
    // Обёртка SFML на opengl функцией glBindTexture
    sf::Texture::bind(&textureData);
    // В uniform кладётся текстурный индекс текстурного блока (для GL_TEXTURE0 - 0, для GL_TEXTURE1 - 1 и тд)
    glUniform1i(unifTexture, 0);

    // Подключаем VBO
    glEnableVertexAttribArray(attribVertex);
    glBindBuffer(GL_ARRAY_BUFFER, vertexVBO);
    glVertexAttribPointer(attribVertex, 3, GL_FLOAT, GL_FALSE, 0, 0);

    glEnableVertexAttribArray(attribTexture);
    glBindBuffer(GL_ARRAY_BUFFER, textureVBO);
    glVertexAttribPointer(attribTexture, 2, GL_FLOAT, GL_FALSE, 0, 0);

    glBindBuffer(GL_ARRAY_BUFFER, 0);

    // Передаем данные на видеокарту(рисуем)
    glDrawArrays(GL_TRIANGLES, 0, 36);

    // Отключаем массив атрибутов
    glDisableVertexAttribArray(attribVertex);
    // Отключаем шейдерную программу
    glUseProgram(0);
    checkOpenGLerror();
}


void ReleaseShader() {
    glUseProgram(0);
    glDeleteProgram(shaderProgram);
}

void ReleaseVBO()
{
    glBindBuffer(GL_ARRAY_BUFFER, 0);
    glDeleteBuffers(1, &vertexVBO);
}

void Release() {
    ReleaseShader();
    ReleaseVBO();
}
