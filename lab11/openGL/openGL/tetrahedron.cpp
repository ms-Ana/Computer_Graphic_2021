#include <gl/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>

#include <iostream>

GLuint Program;

GLint Attrib_vertex;

GLint Attrib_color;

GLint Unif_color;

GLuint VBO_position;

GLuint VBO_color;

struct Vertex
{
	GLfloat x;
	GLfloat y;
	GLfloat z;
};

struct Color
{
	GLfloat R;
	GLfloat G;
	GLfloat B;
	GLfloat A;
};

GLint Unif_translate;
float translate[3] = { 0.0f, 0.0f, 0.0f };

const char* VertexShaderSource = R"(
    #version 330 core
    in vec3 coord;
    in vec4 color;
	uniform vec3 translate;
    out vec4 vert_color;
    void main() {
        vert_color = color;
		vec3 pos = coord;
        gl_Position = vec4(pos[0] + translate[0], pos[1] + translate[1], pos[2] + translate[2], 1.0);
    }
)";

const char* FragShaderSource = R"(
    #version 330 core
    in vec4 vert_color;
    out vec4 color;
    void main() {
        color = vert_color;
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

	while (window.isOpen()) {
		sf::Event event;
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed) {
				window.close();
			}
			else if (event.type == sf::Event::Resized) {
				glViewport(0, 0, event.size.width, event.size.height);
			}

			else if (event.type == sf::Event::KeyPressed) {
				switch (event.key.code) {
				case (sf::Keyboard::Left): translate[0] -= 0.1; break;
				case (sf::Keyboard::Right): translate[0] += 0.1; break;
				case (sf::Keyboard::Down): translate[1] -= 0.1; break;
				case (sf::Keyboard::Up): translate[1] += 0.1; break;
				case (sf::Keyboard::Q): translate[2] += 0.1; break;
				case (sf::Keyboard::A): translate[2] -= 0.1; break;
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


void checkOpenGLerror() {
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error!: " << errCode << std::endl;
}

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

Color HSV2RGB(float H, float S, float V) {
	float s = S / 100;
	float v = V / 100;
	float C = s * v;
	float X = C * (1 - abs(fmod(H / 60.0, 2) - 1));
	float m = v - C;
	float r, g, b;
	if (0 <= H && H < 60) 
		r = C, g = X, b = 0;
	else if (60 <= H && H < 120)
		r = X, g = C, b = 0;
	else if (120 <= H && H < 180) 
		r = 0, g = C, b = X;
	else if (180 <= H && H < 240) 
		r = 0, g = X, b = C;
	else if (240 <= H && H < 300) 
		r = X, g = 0, b = C;
	else 
		r = C, g = 0, b = X;
	
	return { r+m, g+m, b+m, 1.0 };
}

void InitVBO()
{
	glGenBuffers(1, &VBO_position);
	glGenBuffers(1, &VBO_color);

	Vertex triangle[] = {
		{ 0, 0, 0 }, { 1, 0, 0 }, { 0.5, 0.866, 0 },
		{ 0, 0, 0 }, { 0.5, 0.289, 0.577 }, { 0.5, 0.866, 0 },
		{ 1, 0, 0 }, { 0.5, 0.289, 0.577 }, { 0.5, 0.866, 0 },
		{ 0, 0, 0 }, { 0.5, 0.289, 0.577 }, { 1, 0, 0 }
	};

	Color colors[] = {
		HSV2RGB(0, 100, 100), HSV2RGB(120, 100, 100), HSV2RGB(240, 100, 100),
		HSV2RGB(0, 100, 100), HSV2RGB(0, 0, 100), HSV2RGB(240, 100, 100),
		HSV2RGB(120, 100, 100), HSV2RGB(0, 0, 100), HSV2RGB(240, 100, 100),
		HSV2RGB(0, 100, 100), HSV2RGB(0, 0, 100), HSV2RGB(120, 100, 100)
	};
	
	glBindBuffer(GL_ARRAY_BUFFER, VBO_position);
	glBufferData(GL_ARRAY_BUFFER, sizeof(triangle), triangle, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glBufferData(GL_ARRAY_BUFFER, sizeof(colors), colors, GL_STATIC_DRAW);
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

	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);

	glLinkProgram(Program);

	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok)
	{
		std::cout << "error attach shaders \n";
		return;
	}

	Attrib_vertex = glGetAttribLocation(Program, "coord");
	if (Attrib_vertex == -1)
	{
		std::cout << "could not bind attrib coord" << std::endl;
		return;
	}

	Attrib_color = glGetAttribLocation(Program, "color");
	if (Attrib_color == -1)
	{
		std::cout << "could not bind attrib color" << std::endl;
		return;
	}

	const char* unif_name = "translate";
	Unif_color = glGetUniformLocation(Program, unif_name);
	if (Unif_color == -1)
	{
		std::cout << "could not bind " << unif_name << std::endl;
		return;
	}

	checkOpenGLerror();
}

void Init() {
	InitShader();
	InitVBO();
}


void Draw() {
	glUseProgram(Program);

	glUniform3fv(Unif_translate, 1, translate);

	glEnableVertexAttribArray(Attrib_vertex);
	glEnableVertexAttribArray(Attrib_color);

	glBindBuffer(GL_ARRAY_BUFFER, VBO_position);
	glVertexAttribPointer(Attrib_vertex, 3, GL_FLOAT, GL_FALSE, 0, 0);

	glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
	glVertexAttribPointer(Attrib_color, 4, GL_FLOAT, GL_FALSE, 0, 0);

	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glDrawArrays(GL_TRIANGLES, 0, 12);

	glDisableVertexAttribArray(Attrib_vertex);
	glDisableVertexAttribArray(Attrib_color);

	glUseProgram(0);
	checkOpenGLerror();
}


void ReleaseShader() {
	glUseProgram(0);
	glDeleteProgram(Program);
}

void ReleaseVBO()
{
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glDeleteBuffers(1, &VBO_position);
	glDeleteBuffers(1, &VBO_color);
}

void Release() {
	ReleaseShader();
	ReleaseVBO();
}