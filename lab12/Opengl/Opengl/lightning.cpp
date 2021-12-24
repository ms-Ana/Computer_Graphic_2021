
#include <iostream>
#include <gl/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <vector>
#include <string>
#include <fstream>
#include <sstream>

struct vec3 {
	GLfloat x;
	GLfloat y;
	GLfloat z;
};
struct vec2 {
	GLfloat x;
	GLfloat y;
};

struct Vertex {
	// position
	vec3 Position;
	// normal
	vec3 Normal;
};

GLuint Program;

GLint Unif_angle;
GLint unifLightPos;
GLint unifViewVec;

GLuint VBO;
GLuint VAO;
GLuint IBO;

std::vector<Vertex> vertices;
std::vector<vec3> positions;
std::vector<vec3> normals;
std::vector<unsigned int> indices;

float angle[2] = { 1.0f, -1.0f };
void checkOpenGLerror() {
	GLenum errCode;
	// Коды ошибок можно смотреть тут
	// https://www.khronos.org/opengl/wiki/OpenGL_Error
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error!: " << errCode << std::endl;
}
void ShaderLog(unsigned int shader) {
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
void InitShader(const char* vertex_shader, const char* fragment_shader) {
	// Create vertex shader
	GLuint vShader = glCreateShader(GL_VERTEX_SHADER);
	// Link shader source 
	glShaderSource(vShader, 1, &vertex_shader, NULL);
	// Compile shader
	glCompileShader(vShader);
	std::cout << "vertex shader \n";
	ShaderLog(vShader);

	// Create fragment shader
	GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);
	// Link shader source
	glShaderSource(fShader, 1, &fragment_shader, NULL);
	// Compile shader
	glCompileShader(fShader);
	std::cout << "fragment shader \n";
	ShaderLog(fShader);

	// Create program and attach shaders
	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);

	// Link shader program
	glLinkProgram(Program);
	// Check status
	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok)
	{
		std::cout << "error attach shaders \n";
		return;
	}

	const char* unifAngle_name = "angle";
	Unif_angle = glGetUniformLocation(Program, unifAngle_name);
	if (Unif_angle == -1)
	{
		std::cout << "could not bind " << unifAngle_name << std::endl;
		return;
	}

	unifLightPos = glGetUniformLocation(Program, "lightPos");
	if (unifLightPos == -1)
	{
		std::cout << "could not bind uniform lightPos" << std::endl;
		return;
	}
	unifViewVec = glGetUniformLocation(Program, "view");
	if (unifViewVec == -1)
	{
		std::cout << "could not bind uniform view" << std::endl;
		return;
	}
	checkOpenGLerror();
}
void ReleaseShader() {
	// Shader program off
	glUseProgram(0);
	// Delete shader program
	glDeleteProgram(Program);
}
void ReleaseVBO() {
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glDeleteBuffers(1, &VBO);
	glDeleteBuffers(1, &VAO);
	glDeleteBuffers(1, &IBO);
}
void LoadObj(std::string filename) {
	std::ifstream infile(filename);
	std::string instr;

	while (infile >> instr) {
		if (instr == "v")
		{
			float x, y, z;
			infile >> x >> y >> z;
			positions.push_back({ x,y,z });
		}
		else if (instr == "vt")
		{
			float x, y;
			infile >> x >> y;
		}
		else if (instr == "vn")
		{
			float x, y, z;
			infile >> x >> y >> z;
			normals.push_back({ x,y,z });
		}
		else if (instr == "f")
		{
			// get triangle
			for (size_t i = 0; i < 3; i++)
			{
				int p_ind;
				char c;
				// read vertex index
				infile >> p_ind;
				infile >> c;
				// read uv index
				int uv_ind;
				infile >> uv_ind;
				infile >> c;
				// read normal index
				int n_ind;
				infile >> n_ind;
				vertices.push_back({ positions[p_ind - 1],normals[n_ind - 1] });
			}
		}
	}

	indices = std::vector<unsigned int>(vertices.size());

	for (size_t i = 0; i < indices.size(); i++)
	{
		indices[i] = i;
	}
}
void Prepare(std::string filename) {
	LoadObj(filename);

	// create buffers/arrays
	glGenVertexArrays(1, &VAO);
	glGenBuffers(1, &VBO);
	glGenBuffers(1, &IBO);

	glBindVertexArray(VAO);

	// load data into vertex buffers
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(Vertex), &vertices[0], GL_STATIC_DRAW);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, IBO);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.size() * sizeof(unsigned int), &indices[0], GL_STATIC_DRAW);

	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)0);

	glEnableVertexAttribArray(1);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, Normal));

	glBindVertexArray(0);
	checkOpenGLerror();
}

void Init(std::string mesh_filename, const char* vertex_shader, const char* fragment_shader)
{
	InitShader(vertex_shader, fragment_shader);
	Prepare(mesh_filename);
	glEnable(GL_DEPTH_TEST);
}

void Draw(vec3 lightpos, vec3 view) {
	glUseProgram(Program);
	glUniform2fv(Unif_angle, 1, angle);

	glUniform3f(unifViewVec, view.x, view.y, view.z);
	glBindVertexArray(VAO);

	glDrawElements(GL_TRIANGLES, indices.size(), GL_UNSIGNED_INT, 0);

	glBindVertexArray(0);

}

void Release() {
	ReleaseShader();
	ReleaseVBO();
}

const char* VertexShaderSource = R"(
    #version 330 core
    uniform vec2 angle;

    in vec3 coord;
    in vec3 normal;
    
    out vec3 Normal;  
    out vec3 Pos;

    void main() {       
        float x_angle = angle[0];
        float y_angle = angle[1];
        
        mat3 rot =  mat3(
            1, 0, 0,
            0, cos(x_angle), -sin(x_angle),
            0, sin(x_angle), cos(x_angle)
        ) * mat3(
            cos(y_angle), 0, sin(y_angle),
            0, 1, 0,
            -sin(y_angle), 0, cos(y_angle)
        );
       
        vec3 position = coord * rot;
        vec3 normal_ = normal * rot;
        
        gl_Position = vec4(position, 1.0);
        Normal = normal_;
        Pos = position;
    }
    )";
const char* FongFragShaderSource = R"(
        #version 330 core
        out vec4 Color;

        in vec3 Normal;  
        in vec3 Pos;  
  
        uniform vec3 lightPos = vec3(0,0,-1); 
        uniform vec3 view;
     
        void main() {

		const vec3  diffColor = vec3( 0.5, 0.0, 0.0);
		const vec3  specColor = vec3( 0.7, 0.7, 0.0);
		const float specPower = 30.0;
		vec3 n2   = normalize (Normal);
		vec3 l2   = normalize (lightPos);
		vec3 h = lightPos + view;
		vec3 h2   = normalize (h / dot(h, h));
		vec3 diff = diffColor * max (dot(n2,l2), 0.0);
		vec3 spec = specColor * pow (max(dot(n2,h2),0.0), specPower );
		Color = vec4(diff + spec, 1.0);
        }
        )";
const char* ToonShadingFragShaderSource = R"(
        #version 330 core
        out vec4 Color;

        in vec3 Normal;  
        in vec3 Pos;  
  
        uniform vec3 lightPos = vec3(0,0,-1); 
        uniform vec3 view;

        void main() {
		const vec4  diffColor = vec4 ( 0.5, 0.0, 0.0, 1.0 );
		const vec4  specColor = vec4 ( 0.7, 0.7, 0.0, 1.0 );
		const float specPower = 10.0;
		const float edgePower = 3.0;

		vec3  n2   = normalize ( Normal );
		vec3  l2   = normalize ( lightPos );
		float diff = 0.2 + max ( dot ( n2, l2 ), 0.0 );
		vec4  clr;

		if ( diff < 0.4 )
			clr = diffColor * 0.3;
		else
		if ( diff < 0.7 )
			clr = diffColor ;
		else
			clr = diffColor * 1.3;

		Color = clr;
        }
        )";

const char* RimFragShaderSource = R"(
        #version 330 core
        out vec4 Color;

        in vec3 Normal;  
        in vec3 Pos;  
  
        uniform vec3 lightPos = vec3(0,0,-1); 
        uniform vec3 view;

        void main() {
		  const vec4  diffColor = vec4 ( 0.5, 0.0, 0.0, 1.0 );
		  const vec4  specColor = vec4 ( 0.7, 0.7, 0.0, 1.0 );
          const float specPower = 30.0;
          const float rimPower  = 8.0;
          const float bias      = 0.3;

		  vec3  n2   = normalize ( Normal );
		  vec3  l2   = normalize (lightPos);
		  vec3 h = lightPos + view;
          vec3  h2   = normalize ( h / dot(h, h) );
          vec3  v2   = normalize ( view);
          vec4  diff = diffColor * max ( dot ( n2, l2 ), 0.0 );
          vec4  spec = specColor * pow ( max ( dot ( n2, h2 ), 0.0 ), specPower );
          float rim  = pow ( 1.0 + bias - max ( dot ( n2, v2 ), 0.0 ), rimPower );

          Color = diff + rim * vec4 ( 0.5, 0.0, 0.2, 1.0 ) + spec * specColor;
		}
        )";



int main()
{

	sf::Window window(sf::VideoMode(800, 800), "My OpenGL window", sf::Style::Default, sf::ContextSettings(24));
	window.setVerticalSyncEnabled(true);

	window.setActive(true);

	// Инициализация glew
	glewInit();

	Init("cone.obj", VertexShaderSource, FongFragShaderSource);
	//Init("cone.obj", VertexShaderSource, ToonShadingFragShaderSource);
	//Init("cone.obj", VertexShaderSource, RimFragShaderSource);

	vec3 lightPos = { 1.0f, 1.0f, 1.0f };
	vec3 view = { 1.0f, 0.0f, 0.0f };

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
				case (sf::Keyboard::Left): angle[1] -= 0.1; break;
				case (sf::Keyboard::Right): angle[1] += 0.1; break;
				case (sf::Keyboard::Down): angle[0] -= 0.1; break;
				case (sf::Keyboard::Up): angle[0] += 0.1; break;
				default: break;
				}
			}
		}

		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		Draw(lightPos, view);

		window.display();
	}

	Release();
	return 0;
}

